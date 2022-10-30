from datetime import datetime
from unittest import TestCase
from datetime import date as date

import databaseConnector


class Test(TestCase):
    def test_is_task_updated_in_db(self):
        date1 = "2022-07-20"

        row = {
            "taskName": "test",
            "leadtime": 11,
            "column": "test",
            "timeStamp": datetime.strptime(date1, '%Y-%m-%d'),
            "taskID": 11111,
            "daysActive": 10
        }

        databaseConnector.isTaskUpdatedInDB(row)

    def test_get_result_count(self):
        query = f"SELECT * FROM kanban WHERE task_id='11111'"
        len = databaseConnector.getResultCount(query)
        print(len)

