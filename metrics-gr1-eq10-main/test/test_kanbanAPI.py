from unittest import TestCase

import kanbanAPI


class Test(TestCase):


    def test_get_all_column_ids(self):
        print(kanbanAPI.getAllColumnIDs())

    def test_get_tasks_by_column(self):
        kanbanAPI.getTasksByColumn(18671780)
        kanbanAPI.getTasksByColumn(18671775)
        print(kanbanAPI.getAllColumnIDs())

