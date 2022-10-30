from datetime import datetime, time

import pymysql.cursors  # pylint: disable=import-error


def connectToDb():
    return pymysql.connect(
        host="hvac-gr1-eq10.cum5ethud67f.us-east-1.rds.amazonaws.com",
        user="admin",
        password="LOG680lab3#?",
        db="hvac-gr1-eq10",
        port=3306,
    )


def genericQueryToDB(query):
    connection = connectToDb()
    try:
        with connection:
            with connection.cursor() as cursor:
                cursor.execute(query)
            connection.commit()

            return True
    except Exception as err:
        print(err)
        return False


def selectQueryToDB(query):
    connection = connectToDb()
    try:
        with connection:
            with connection.cursor() as cursor:
                cursor.execute(query)
                connection.commit()

                return cursor.fetchall()
    except Exception as err:
        print(err)
        return False


def getResultCount(query):
    return len(selectQueryToDB(query))


def isTaskUpdatedInDB(row):
    taskID = row["taskID"]
    query = f"SELECT * FROM kanban WHERE task_id='{taskID}'"
    res = selectQueryToDB(query)[0]

    dt = datetime(res[5].year, res[5].month, res[5].day)

    if (res[2] != row["taskName"]
            or res[3] != row["leadtime"]
            or res[4] != row["column"]
            or dt != row["timeStamp"]
            or res[6] != row["daysActive"]):
        return True
    else:
        return False

def isPRUpdatedInDB(row):
    pr_id = row["pr_id"]
    res = selectQueryToDB(f"SELECT * FROM pull_request WHERE pr_id='{pr_id}'")[0]

    if (res[2] != row["temps"]
            or res[3] != row["pr_name"]
            or res[4] != row["modified_lines"]):
        return True
    else:
        return False


def isIntegrationUpdatedInDB(row):
    runId = row["run_id"]
    res = selectQueryToDB(f"SELECT * FROM integration_continue WHERE run_id='{runId}'")[0]

    if (res[2] != row["run_name"]
            or res[3] != row["workflow_associated"]
            or res[4] != row["test_time"]
            or res[5] != row["test_status"]
            or res[6] != row["build_time"]):
        return True
    else:
        return False
