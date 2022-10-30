import requests


def getRequest(url):
    username = "DavidOlivierBouchard"
    token = "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"

    response = requests.get(url, auth=(username, token))
    return response


def getAllPR():
    res = getRequest(f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/pulls?state=all").json()

    return res

def getModifiedLinesCount(prNumber):
    pr = getRequest(f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/pulls/{prNumber}").json()

    return pr["additions"] + pr["deletions"]