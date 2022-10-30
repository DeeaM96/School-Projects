import 'dotenv/config';
import cors from 'cors';
import express from 'express';
import request from 'request';
import fs from 'fs';
import crypto from 'crypto';
import { parse } from 'csv-parse';
import { DAILY_DATA_COLUMNS, MAPPING_STATION } from './constants.js';
import { Parser } from 'xml2js';

const app = express();

const download = (uri, filename, callback) => {
  request.head(uri, function (err, res, body) {
    console.log('content-type:', res.headers['content-type']);
    console.log('content-length:', res.headers['content-length']);

    request(uri)
      .pipe(fs.createWriteStream(`tmp_files/${filename}`))
      .on('close', callback);
  });
};

app.use(cors());

app.get('/daily-data/:idStation', (req, res) => {
  const { idStation } = req.params;
  const { year, month, day } = req.query;
  const climateURL = `https://climate.weather.gc.ca/climate_data/bulk_data_e.html?format=csv&stationID=${idStation}&Year=${year}&Month=${month}&Day=${day}&timeframe=1&submit=%20Download+Data`;
  const fileName = `data_${crypto.randomUUID().toString()}.csv`;

  download(climateURL, fileName, async () => {
    console.log('download done');
    let csvData = [];
    fs.createReadStream(`tmp_files/${fileName}`)
      .pipe(parse({ delimiter: ',', relaxQuotes: true }))
      .on('data', (csvrow) => {
        csvData.push(csvrow);
      })
      .on('end', async () => {
        const dataTable = [];
        // first row of csv file is the header
        csvData.shift();

        // iterate and parse data
        csvData.forEach((csvRow) => {
          let dailyData = Object.assign(
            {},
            ...DAILY_DATA_COLUMNS.map((x, index) => ({
              [x]: csvRow[index],
            }))
          );
          // push data to array
          dataTable.push(dailyData);
        });

        // Delete CSV file from server
        fs.unlink(`tmp_files/${fileName}`, (err) => {
          if (err) console.log(err);
          else {
            console.log(`DELETED tmp_files/${fileName}`);
            // return data
            console.log(`/daily-data/${idStation} DONE`);
            res.json(dataTable).status(200);
          }
        });
      });
  });
});

app.get('/prevision/:airportCode', (req, res) => {
  const { airportCode } = req.params;
  try {
    const apiLink =
      MAPPING_STATION[airportCode.trim().toUpperCase()]['rss_feed'];
    console.log(apiLink);
    const fileName = `prevision_${crypto.randomUUID().toString()}.xml`;

    download(apiLink, fileName, async () => {
      console.log('download done');
      const fileData = fs.readFileSync(`tmp_files/${fileName}`, 'ascii');
      const parser = new Parser();

      parser.parseString(
        fileData.substring(0, fileData.length),
        (err, result) => {
          if (err) {
            return res.send('XML parsing failed').status(400);
          }
          console.log('xml parsed');

          // Delete XML file from server
          fs.unlink(`tmp_files/${fileName}`, (err) => {
            if (err) console.log(err);
            else {
              console.log(`DELETED tmp_files/${fileName}`);
              // return data
              console.log(`/prevision/${airportCode} DONE`);
              res.json(result).status(200);
            }
          });
        }
      );
    });
  } catch (e) {
    res.send('Invalid airport code').status(400);
  }
});

app.listen(process.env.PORT, () =>
  console.log(`GTI525 listening on PORT ${process.env.PORT}!`)
);
