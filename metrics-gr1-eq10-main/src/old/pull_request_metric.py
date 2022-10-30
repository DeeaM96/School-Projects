from old.api import pull_request_api
import datetime


def average_time_all_pr(org_name, repo_name):
    res = pull_request_api.get_request(f"https://api.github.com/repos/{org_name}/{repo_name}/pulls?state=all")

    total_nb = len(res.json())
    pull_requests = res.json()

    total_time = 0
    for pr in pull_requests:
        if pr["state"] == "closed":
            created_at = datetime.datetime.strptime(pr["created_at"], '%Y-%m-%dT%H:%M:%SZ')
            closed_at = datetime.datetime.strptime(pr["closed_at"], '%Y-%m-%dT%H:%M:%SZ')
            diff = (closed_at - created_at).total_seconds() / 3600
            total_time += diff;

    average_time = total_time / total_nb
    print(f"Temps moyen des pull requests: {round(average_time, 2)} heures")


def menu_closed_prs(org_name, repo_name):
    res = pull_request_api.get_request(f"https://api.github.com/repos/{org_name}/{repo_name}/pulls?state=all")
    pull_requests = res.json()

    i = 1;
    print("Voici la liste des pull request fermés")

    for pr in pull_requests:
        if pr["state"] == "closed":
            print(f"{i}. {pr['title']}")
            i += 1


def select_closed_pr_for_time(org_name, repo_name, selected_closed_pr):
    res = pull_request_api.get_request(f"https://api.github.com/repos/{org_name}/{repo_name}/pulls?state=all")
    pull_requests = res.json()
    i = 1;
    total_closed_pr = 0
    closed_prs = []

    for pr in pull_requests:
        if pr["state"] == "closed":
            total_closed_pr += 1;
            closed_prs.append(pr)

    if check_is_digit(selected_closed_pr):
        if total_closed_pr > int(selected_closed_pr) - 1 > 0:
            pr = closed_prs[int(int(selected_closed_pr) - 1)]
            created_at = datetime.datetime.strptime(pr["created_at"], '%Y-%m-%dT%H:%M:%SZ')
            closed_at = datetime.datetime.strptime(pr["closed_at"], '%Y-%m-%dT%H:%M:%SZ')
            diff = (closed_at - created_at).total_seconds() / 3600

            print(f"{pr['title']} : {round(diff, 2)} heures")

        else:
            print("choix invalide")
    else:
        print("choix invalide")


def total_pr(org_name, repo_name):
    res = pull_request_api.get_request(f"https://api.github.com/repos/{org_name}/{repo_name}/pulls?state=all")
    print(len(res.json()))
    total_nb = len(res.json());
    print("Nombre total de pull requests :", total_nb)


def menu_all_prs(org_name, repo_name):
    res = pull_request_api.get_request(f"https://api.github.com/repos/{org_name}/{repo_name}/pulls?state=all")
    pull_requests = res.json()

    i = 1;
    global total_pull_requests
    total_pull_requests = 0
    print("Entrez le numero du pull request")
    for pr in pull_requests:
        print(f"{i}. {pr['title']}")
        i += 1
        total_pull_requests += 1;


def total_reviews(org_name, repo_name):
    res = pull_request_api.get_request(f"https://api.github.com/repos/{org_name}/{repo_name}/pulls?state=all")
    pull_requests = res.json()

    total_comments = 0
    for pr in pull_requests:
        comments = pull_request_api.get_request(
            f"https://api.github.com/repos/{org_name}/{repo_name}/pulls/{pr['number']}/comments")
        total_comments += len(comments.json())

    print(f"Nombre total de reviews: {total_comments}")

def check_is_digit(input_str):
    if input_str.strip().isdigit():
        return True
    else:
        return False


def select_pr_for_reviews(org_name, repo_name,selected_pr):
    res = pull_request_api.get_request(f"https://api.github.com/repos/{org_name}/{repo_name}/pulls?state=all")
    pull_requests = res.json()
    i=1;
    total_pr=len(res.json())
    prs=[]

    if check_is_digit(selected_pr):
        if total_pr > int(selected_pr)-1 > 0:
            pr=pull_requests[int(int(selected_pr)-1)]
            comments = pull_request_api.get_request(f"https://api.github.com/repos/{org_name}/{repo_name}/pulls/{pr['number']}/comments")
            print(f"{pr['title']} : {len(comments.json())} commentaires" )

        else:
            print("choix invalide")
    else:
        print("choix invalide")