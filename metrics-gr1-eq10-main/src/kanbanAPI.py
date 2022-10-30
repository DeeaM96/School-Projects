import datetime
import requests


def getRequest(url):
    username = "DavidOlivierBouchard"
    token = "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"

    response = requests.get(url, auth=(username, token))
    return response


def getAllColumnIDs():
    return getRequest(f"https://api.github.com/projects/14448457/columns").json()


def getTasksByColumn(cid):
    res = getRequest(f"https://api.github.com/projects/columns/{cid}/cards")
    tasks = res.json()
    if res.links is not None:
        for link in res.links.values():
            if link['rel'] == 'next':
                tasks = tasks + (getRequest(link['url']).json())

    return tasks


def getTaskDetail(contentURL):
    return getRequest(contentURL).json()
