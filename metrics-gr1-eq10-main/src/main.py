import kanbanAPI
import pullRequestAPI
import integrationAPI
from datetime import datetime
from src import databaseConnector as db


class Main:
    def __init__(self):
        self.connection = db.connectToDb()

    def start(self):
        self.syncKanbanTable()
        self.syncPullRequestDatabase()
        self.syncIntegrationContinueDatabase()
        print("Done !")

    def syncKanbanTable(self):
        print("synchronisation BD métriques Kanban en cours ...")

        columns = kanbanAPI.getAllColumnIDs()

        for column in columns:
            cid = column["id"]
            columnName = column["name"]

            columnTasks = kanbanAPI.getTasksByColumn(cid)

            if len(columnTasks) > 0:
                for t in columnTasks:
                    task = kanbanAPI.getTaskDetail(t["content_url"])
                    taskName = task["title"]
                    taskUpdatedTime = datetime.strptime(task["updated_at"], '%Y-%m-%dT%H:%M:%SZ')
                    taskCreatedTime = datetime.strptime(task["created_at"], '%Y-%m-%dT%H:%M:%SZ')
                    taskLeadTime = (datetime.now() - taskCreatedTime).total_seconds() / 3600
                    taskTimeStamp = taskUpdatedTime
                    taskID = task["id"]
                    taskDaysActive = (datetime.now() - taskUpdatedTime).days

                    row = {
                        "taskName": taskName,
                        "leadtime": taskLeadTime,
                        "column": columnName,
                        "timeStamp": taskTimeStamp,
                        "taskID": taskID,
                        "daysActive": taskDaysActive
                    }

                    taskName = taskName.replace("'", "\\'")  # erreur dans la query SQL avec les "'".

                    if db.getResultCount(f"SELECT * FROM kanban WHERE task_id='{taskID}'") > 0:
                        if db.isTaskUpdatedInDB(row):
                            db.genericQueryToDB(
                                f"UPDATE kanban SET task_name='{taskName}', lead_time='{taskLeadTime}', "
                                f"column_name='{columnName}', timestamp='{taskTimeStamp}', days_active='{taskDaysActive}' "
                                f"WHERE task_id='{taskID}'")
                    else:
                        db.genericQueryToDB(
                            f"INSERT INTO kanban (task_id, task_name, lead_time, column_name, timestamp, days_active) "
                            f"VALUES ('{taskID}', '{taskName}', '{taskLeadTime}', '{columnName}', '{taskTimeStamp}', '{taskDaysActive}')")

    def syncPullRequestDatabase(self):
        print("synchronisation BD métriques Pull Request en cours ...")

        prs = pullRequestAPI.getAllPR()

        for pr in prs:
            name = pr["title"]
            pid = pr["id"]

            prCreatedTime = datetime.strptime(pr["created_at"], '%Y-%m-%dT%H:%M:%SZ')
            prUpdatedTime = datetime.strptime(pr["updated_at"], '%Y-%m-%dT%H:%M:%SZ')
            prTime = round((prUpdatedTime - prCreatedTime).total_seconds() / 60)

            nbLines = pullRequestAPI.getModifiedLinesCount(pr["number"])

            row = {
                "pr_id": pid,
                "pr_name": name,
                "temps": prTime,
                "modified_lines": nbLines
            }

            name = name.replace("'", "\\'")

            if db.getResultCount(f"SELECT * FROM pull_request WHERE pr_id='{pid}'") > 0:
                if db.isPRUpdatedInDB(row):
                    db.genericQueryToDB(
                        f"UPDATE pull_request SET pr_id='{pid}', pr_name='{name}', temps='{prTime}', "
                        f"modified_lines='{nbLines}' WHERE pr_id='{pid}'")
            else:
                db.genericQueryToDB(
                    f"INSERT INTO pull_request (pr_id, pr_name, temps, modified_lines) "
                    f"VALUES ('{pid}', '{name}', '{prTime}', '{nbLines}')")

    def syncIntegrationContinueDatabase(self):
        print("synchronisation BD métriques Intégration Continue en cours ...")

        workflows = integrationAPI.getAllWorkflows()

        for workflow in workflows:
            workflowName = workflow["name"]

            runs = integrationAPI.getRunsByWorkflow(workflow["id"])
            for run in runs:
                runId = run["id"]
                runName = run["head_commit"]["message"]

                testStartTime = integrationAPI.getTestStartTime(runId)
                testEndTime = integrationAPI.getTestEndtime(runId)

                if testStartTime is None or testEndTime is None:
                    testTime = 0
                else:
                    testTime = (testEndTime - testStartTime).total_seconds()

                testStatus = integrationAPI.getTestStatus(runId)
                buildTime = integrationAPI.getRunsBuildTime(runId)

                row = {
                    "run_id": runId,
                    "run_name": runName,
                    "workflow_associated": workflowName,
                    "test_time": testTime,
                    "test_status": testStatus,
                    "build_time": buildTime
                }

                runName = runName.replace("'", "\\'")
                workflowName = workflowName.replace("'", "\\'")

                if db.getResultCount(f"SELECT * FROM integration_continue WHERE run_id='{runId}'") > 0:
                    if db.isIntegrationUpdatedInDB(row):
                        db.genericQueryToDB(
                            f"UPDATE integration_continue SET run_id='{runId}', run_name='{runName}', workflow_associated='{workflowName}', "
                            f"test_time='{testTime}', test_status='{testStatus}', build_time='{buildTime}' WHERE run_id='{runId}'")
                else:
                    db.genericQueryToDB(
                        f"INSERT INTO integration_continue (run_id, run_name, workflow_associated, test_time, test_status, build_time) "
                        f"VALUES ('{runId}', '{runName}', '{workflowName}', '{testTime}', '{testStatus}', '{buildTime}')")




if __name__ == "__main__":
    main = Main()
    main.start()
