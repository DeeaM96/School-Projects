const DAILY_DATA_COLUMNS = [
  'longitude',
  'latitude',
  'stationName',
  'climateId',
  'dateTime',
  'year',
  'month',
  'day',
  'time',
  'temp',
  'tempFlag',
  'dewPointTemp',
  'dewPointTempFlag',
  'relHum',
  'relHumFlag',
  'precipAmount',
  'precipAmountFlag',
  'windDir',
  'windDirFlag',
  'windsPd',
  'windsPdFlag',
  'visibility',
  'visibilityFlag',
  'stnPress',
  'stnPressFlag',
  'hmdx',
  'hmdxFlag',
  'windChill',
  'windChillFlag',
  'weather',
];

const MAPPING_STATION = {
  YYC: {
    station_ids: [50430, 2205],
    rss_feed: 'https://meteo.gc.ca/rss/city/ab-52_f.xml',
  },
  YEG: {
    station_ids: [1865],
    rss_feed: 'https://meteo.gc.ca/rss/city/ab-50_f.xml',
  },
  YQX: {
    station_ids: [6633],
    rss_feed: 'https://meteo.gc.ca/rss/city/nl-16_f.xml',
  },
  YQM: {
    station_ids: [6207],
    rss_feed: 'https://meteo.gc.ca/rss/city/nb-36_f.xml',
  },
  YHZ: {
    station_ids: [53938, 6358, 50620],
    rss_feed: 'https://meteo.gc.ca/rss/city/ns-19_f.xml',
  },
  YHM: {
    station_ids: [49908, 4932],
    rss_feed: 'https://meteo.gc.ca/rss/city/on-77_f.xml',
  },
  YXU: {
    station_ids: [50093, 4789],
    rss_feed: 'https://meteo.gc.ca/rss/city/on-137_f.xml',
  },
  YUL: {
    station_ids: [5415, 51157],
    rss_feed: 'https://meteo.gc.ca/rss/city/qc-147_f.xml',
  },
  YOW: {
    station_ids: [4337, 49568],
    rss_feed: 'https://meteo.gc.ca/rss/city/on-118_f.xml',
  },
  YQB: {
    station_ids: [51457, 5251],
    rss_feed: 'https://meteo.gc.ca/rss/city/qc-133_f.xml',
  },
  YQR: {
    station_ids: [3002, 51441],
    rss_feed: 'https://meteo.gc.ca/rss/city/sk-32_f.xml',
  },
  YXE: {
    station_ids: [3328, 50091],
    rss_feed: 'https://meteo.gc.ca/rss/city/sk-40_f.xml',
  },
  YYT: {
    station_ids: [50089, 6720],
    rss_feed: 'https://meteo.gc.ca/rss/city/nl-24_f.xml',
  },
  YYZ: {
    station_ids: [51459, 5097],
    rss_feed: 'https://meteo.gc.ca/rss/city/on-143_f.xml',
  },
  YVR: {
    station_ids: [51357, 51442],
    rss_feed: 'https://meteo.gc.ca/rss/city/bc-74_f.xml',
  },
  YYJ: {
    station_ids: [118, 51337],
    rss_feed: 'https://meteo.gc.ca/rss/city/bc-85_f.xml',
  },
  YWG: {
    station_ids: [3698, 47407, 51097],
    rss_feed: 'https://meteo.gc.ca/rss/city/mb-38_f.xml',
  },
};

export { DAILY_DATA_COLUMNS, MAPPING_STATION };
