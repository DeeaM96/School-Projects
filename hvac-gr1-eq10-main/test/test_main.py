import unittest
import sys
from unittest import mock
import requests
import os
from datetime import datetime


class TestStringMethods(unittest.TestCase):

    @mock.patch.dict(os.environ, {"HVAC_HOST": "http://35.203.86.116"})
    def test_simulator_up(self):
        HOST = os.environ["HVAC_HOST"]
        r = requests.get(f"{HOST}/api/health")
        self.assertEqual("All system operational Commander !", r.text)

    @mock.patch.dict(os.environ, {"HVAC_HOST": "http://1.1.1.1", "HVAC_TOKEN": "token"})
    def test_host_value(self):
        sys.path.insert(1, 'src/')
        import main
        self.assertEqual("http://1.1.1.1", main.Main().HOST)

    @mock.patch.dict(os.environ, {"HVAC_TOKEN": "token"})
    def test_host_default(self):
        sys.path.insert(1, 'src/')
        import main
        self.assertEqual("http://35.203.86.116", main.Main().HOST)

    def test_no_token(self):
        sys.path.insert(1, 'src/')
        import main
        with self.assertRaises(KeyError):
            main.Main().setup()

    @mock.patch.dict(os.environ, {"HVAC_TOKEN": "token"})
    def test_token_value(self):
        sys.path.insert(1, 'src/')
        import main
        self.assertEqual("token", main.Main().TOKEN)

    @mock.patch.dict(os.environ, {"COLD_TH": "10", "HVAC_TOKEN": "token"})
    def test_cold_value(self):
        sys.path.insert(1, 'src/')
        import main
        self.assertEqual(10, main.Main().COLD_TH)

    @mock.patch.dict(os.environ, {"HVAC_TOKEN": "token"})
    def test_cold_default_value(self):
        sys.path.insert(1, 'src/')
        import main
        self.assertEqual(15, main.Main().COLD_TH)

    @mock.patch.dict(os.environ, {"HOT_TH": "200", "HVAC_TOKEN": "token"})
    def test_hot_value(self):
        sys.path.insert(1, 'src/')
        import main
        self.assertEqual(200, main.Main().HOT_TH)

    @mock.patch.dict(os.environ, {"HVAC_TOKEN": "token"})
    def test_cold_default_value(self):
        sys.path.insert(1, 'src/')
        import main
        self.assertEqual(25, main.Main().HOT_TH)

    @mock.patch.dict(os.environ, {"NB_TICKS": "5", "HVAC_TOKEN": "token"})
    def test_ticks_value(self):
        sys.path.insert(1, 'src/')
        import main
        self.assertEqual(5, main.Main().NB_TICKS)

    @mock.patch.dict(os.environ, {"HVAC_TOKEN": "token"})
    def test_ticks_default_value(self):
        sys.path.insert(1, 'src/')
        import main
        self.assertEqual(6, main.Main().NB_TICKS)

    @mock.patch.dict(os.environ, {"HVAC_TOKEN": "token"})
    def test_db_connection(self):
        sys.path.insert(1, 'src/')
        import main
        # Get connection to DB
        connection = main.Main().connectToDb()

        # Test to see if it is opened
        self.assertEqual(connection.open, True)

    @mock.patch.dict(os.environ, {"HVAC_TOKEN": "token"})
    def test_db_insert_temperature(self):
        sys.path.insert(1, 'src/')
        import main
        # Data used for testing
        timestamp = datetime.now()
        temperature = 400.40
        connection = main.Main().connectToDb()
        self.assertEqual(main.Main().saveTemperatureToDB(timestamp, temperature), True)
        time = timestamp.strftime("%y-%m-%d %H:%M:%S")
        self.delete_temperature_from_DB(main.Main().connectToDb(), time, temperature), True

    @mock.patch.dict(os.environ, {"HVAC_TOKEN": "token"})
    def test_db_insert_event(self):
        sys.path.insert(1, 'src/')
        import main
        # Data used for testing
        timestamp = datetime.now()
        hvacTestEvent = "TestHVAC"
        testingTicks = 6
        self.assertEqual(main.Main().saveEventToDB(timestamp, hvacTestEvent, testingTicks), True)
        time = timestamp.strftime("%y-%m-%d %H:%M:%S")
        self.delete_event_from_DB(main.Main().connectToDb(), time, hvacTestEvent), True


    def delete_temperature_from_DB(self, connection, time, temperature):
        try:
            with connection:
                with connection.cursor() as cursor:

                    delsql = f"DELETE FROM `temperature` WHERE timestamp='{time}' AND temperature='{temperature}'"
                    cursor.execute(delsql)

                connection.commit()
                return True

        except Exception as err:
            print(err)
            return False

    def delete_event_from_DB(self, connection, time, hvacTestingEvent):

        try:
            with connection:
                with connection.cursor() as cursor:

                    delsql = f"DELETE FROM `event` WHERE timestamp='{time}' AND event='{hvacTestingEvent}'"
                    cursor.execute(delsql)

                connection.commit()
                return True

        except Exception as err:
            print(err)
            return False





if __name__ == '__main__':
    unittest.main()
