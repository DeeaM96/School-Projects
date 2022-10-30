
import logging
import sys
import json
import time
import os
from datetime import datetime
from signalrcore.hub_connection_builder import HubConnectionBuilder
import requests  # pylint: disable=import-error
import pymysql.cursors  # pylint: disable=import-error



class Main:
    def __init__(self):
        self._hub_connection = None
        self.HOST = os.environ.get("HVAC_HOST", "http://35.203.86.116")
        self.TOKEN = os.environ["HVAC_TOKEN"]
        self.COLD_TH = float(os.environ.get("COLD_TH", "15"))
        self.HOT_TH = float(os.environ.get("HOT_TH", "25"))
        self.NB_TICKS = int(os.environ.get("NB_TICKS", "6"))

        self.connection = self.connectToDb()

    def __del__(self):
        if self._hub_connection != None:
            self._hub_connection.stop()

    def setup(self):
        self.setSensorHub()

    def start(self):
        self.setup()
        self._hub_connection.start()

        print("Press CTRL+C to exit.")
        while True:
            time.sleep(2)

        self._hub_connection.stop()
        sys.exit(0)

    def setSensorHub(self):
        self._hub_connection = (
            HubConnectionBuilder()
            .with_url(f"{self.HOST}/SensorHub?token={self.TOKEN}")
            .configure_logging(logging.INFO)
            .with_automatic_reconnect(
                {
                    "type": "raw",
                    "keep_alive_interval": 10,
                    "reconnect_interval": 5,
                    "max_attempts": 999,
                }
            )
            .build()
        )

        self._hub_connection.on("ReceiveSensorData", self.onSensorDataReceived)
        self._hub_connection.on_open(lambda: print("||| Connection opened."))
        self._hub_connection.on_close(lambda: print("||| Connection closed."))
        self._hub_connection.on_error(
            lambda data: print(f"||| An exception was thrown closed: {data.error}")
        )

    def onSensorDataReceived(self, data):
        try:
            print(data[0]["date"] + " --> " + data[0]["data"])

            date = data[0]["date"]
            dp = float(data[0]["data"])

            self.saveTemperatureToDB(date, dp)
            self.analyzeDatapoint(date, dp)
        except Exception as err:
            print(err)

    def analyzeDatapoint(self, date, data):
        if data >= self.HOT_TH:
            self.sendActionToHvac(date, "TurnOnAc", self.NB_TICKS)
        elif data <= self.COLD_TH:
            self.sendActionToHvac(date, "TurnOnHeater", self.NB_TICKS)

    def sendActionToHvac(self, date, action, nbTick):
        r = requests.get(f"{self.HOST}/api/hvac/{self.TOKEN}/{action}/{nbTick}")
        details = json.loads(r.text)
        print(details)
        self.saveEventToDB(datetime.now(), action, nbTick)

    def saveEventToDB(self, timestamp, event, nbTick):
        connection = self.connectToDb()
        try:
            with connection:
                with connection.cursor() as cursor:

                    # Create a new record
                    sql = "INSERT INTO `event` (`timestamp`, `event`, `nbTick`) VALUES (%s, %s, %s)"
                    cursor.execute(sql, (timestamp, event, nbTick))

                # connection is not autocommit by default. So you must commit to save
                # your changes.
                connection.commit()

                return True
        except Exception as err:
            print(err)
            return False

    def saveTemperatureToDB(self, timestamp, temperature):
        connection = self.connectToDb()
        # timestamp = timestamp.str
        try:
            with connection:
                with connection.cursor() as cursor:

                    # Create a new record
                    sql = "INSERT INTO `temperature` (`timestamp`, `temperature`) VALUES (%s, %s)"
                    cursor.execute(sql, (timestamp, temperature))

                # connection is not autocommit by default. So you must commit to save
                # your changes.
                connection.commit()
                return True

        except Exception as err:
            print(err)
            return False

    def connectToDb(self):
        return pymysql.connect(
            host="hvac-gr1-eq10.cum5ethud67f.us-east-1.rds.amazonaws.com",
            user="admin",
            password="LOG680lab3#?",
            db="hvac-gr1-eq10",
            port=3306,
        )


if __name__ == "__main__":
    main = Main()
    main.start()
