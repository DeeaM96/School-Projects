from unittest import TestCase

from main import Main
from old import menu


class TestMain(TestCase):

    def test_sync_Kanban_Table(self):
        Main.syncKanbanTable(Main)

    def test_menu(self):
        menu.main()


    def test_sync_pr(selfs):
        Main.syncPullRequestDatabase(Main)

    def test_sync_Integration_Continue_Database(self):
        Main.syncIntegrationContinueDatabase(Main)

