from datetime import datetime

import requests


def getRequest(url):
    username = "DavidOlivierBouchard"
    token = "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"

    response = requests.get(url, auth=(username, token))
    return response


def getAllWorkflows():
    workflows = getRequest(f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/workflows").json()
    return workflows["workflows"]



def getRunsByWorkflow(workflowID):
    res = getRequest(f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/workflows/{workflowID}/runs")
    runs = res.json()["workflow_runs"]
    if res.links is not None:
        url = res.links['next']['url'].split('=')[0]
        x = int(res.links['next']['url'].split('=')[1])
        y = int(res.links['last']['url'].split('=')[1])


        for i in range(x, y+1):
            link = url+"="+str(i)
            runs = runs + (getRequest(link).json()["workflow_runs"])

    return runs

def getRunsBuildTime(runID):
    jobs = getRequest(f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/runs/{runID}/jobs").json()[
        "jobs"]

    buildTime = 0

    for job in jobs:
        if job['name'].find('build') != -1:
            run = getRequest(job['check_run_url']).json()
            created_at = datetime.strptime(run["started_at"], '%Y-%m-%dT%H:%M:%SZ')
            closed_at = datetime.strptime(run["completed_at"], '%Y-%m-%dT%H:%M:%SZ')

            buildTime = (closed_at - created_at).total_seconds()

    return buildTime


def getTestStartTime(runId):
    jobs = getRequest(f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/runs/{runId}/jobs").json()["jobs"]

    for job in jobs:
        if job['name'].find('test') != -1 and job['status'] == 'completed':
            return datetime.strptime(job["started_at"], '%Y-%m-%dT%H:%M:%SZ')
        else:
            return None


def getTestEndtime(runId):
    jobs = getRequest(f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/runs/{runId}/jobs").json()["jobs"]

    for job in jobs:
        if job['name'].find('test') != -1 and job['status'] == 'completed':
            return datetime.strptime(job["completed_at"], '%Y-%m-%dT%H:%M:%SZ')
        else:
            return None


def getTestStatus(runId):
    jobs = getRequest(f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/runs/{runId}/jobs").json()[
        "jobs"]

    for job in jobs:
        if job['name'].find('test') != -1:
            return job["conclusion"]
        else:
            return "N.A."
