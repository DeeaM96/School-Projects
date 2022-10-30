from old.api import kanban_api
import datetime
import requests


def get_request(url):
    username = "DavidOlivierBouchard"
    token = "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"

    response = requests.get(url, auth=(username, token))
    return response

def get_all_issues():
    backlog = kanban_api.get_request(f"https://api.github.com/projects/columns/18671748/cards").json()
    to_do = kanban_api.get_request(f"https://api.github.com/projects/columns/18671752/cards").json()
    in_progress = kanban_api.get_request(f"https://api.github.com/projects/columns/18671753/cards").json()
    to_be_reviewed = kanban_api.get_request(f"https://api.github.com/projects/columns/18671754/cards").json()
    done = kanban_api.get_request(f"https://api.github.com/projects/columns/18671761/cards").json()

    tasks = []
    for card in backlog:
        issue = kanban_api.get_request(card["content_url"]).json()

        tasks.append(issue)
    for card in to_do:
        issue = kanban_api.get_request(card["content_url"]).json()

        tasks.append(issue)
    for card in in_progress:
        issue = kanban_api.get_request(card["content_url"]).json()

        tasks.append(issue)
    for card in to_be_reviewed:
        issue = kanban_api.get_request(card["content_url"]).json()

        tasks.append(issue)
    for card in done:
        issue = kanban_api.get_request(card["content_url"]).json()

        tasks.append(issue)


    return tasks

def print_tasks(tasks):
    i = 1
    for task in tasks:
        print(f"{i}. {task['title']}")
        i += 1


def select_task_for_time(selected_task, tasks):
    total_tasks = len(tasks)

    if check_is_digit(selected_task):
        if total_tasks > int(selected_task) - 1 >= 0:
            task = tasks[int(int(selected_task) - 1)]
            created_at = datetime.datetime.strptime(task["created_at"], '%Y-%m-%dT%H:%M:%SZ')
            updated_at = datetime.datetime.strptime(task["updated_at"], '%Y-%m-%dT%H:%M:%SZ')
            diff = (updated_at - created_at).total_seconds() / 3600

            if round(diff, 2) == 0.0:
                diff_minutes = (updated_at - created_at).total_seconds() / 60
                print(f"{task['title']} : {diff_minutes} minutes")
            else:
                print(f"{task['title']} : {round(diff, 2)} heures")
        else:
            print("choix invalide")
    else:
        print("choix invalide")


def get_done_issues():
    print()
    done = kanban_api.get_request(f"https://api.github.com/projects/columns/18671761/cards").json()

    tasks = []
    for card in done:
        issue = kanban_api.get_request(card["content_url"]).json()
        tasks.append(issue)

    return tasks


def get_tasks_in_between_dates(start_date, end_date, tasks):
    tasks_between_dates = []
    start_date = f"{start_date} 00:00:00"
    end_date = f"{end_date} 23:59:59"

    for task in tasks:
        created_at = datetime.datetime.strptime(task["created_at"], '%Y-%m-%dT%H:%M:%SZ')
        updated_at = datetime.datetime.strptime(task["updated_at"], '%Y-%m-%dT%H:%M:%SZ')
        if created_at >= datetime.datetime.strptime(start_date,'%Y-%m-%d 00:00:00') \
                and updated_at <= datetime.datetime.strptime(end_date, '%Y-%m-%d 23:59:59'):
            tasks_between_dates.append(task)
    return tasks_between_dates


def show_times_for_tasks(tasks):
    for task in tasks:
        created_at = datetime.datetime.strptime(task["created_at"], '%Y-%m-%dT%H:%M:%SZ')
        closed_at = datetime.datetime.strptime(task["closed_at"], '%Y-%m-%dT%H:%M:%SZ')

        diff = (closed_at - created_at).total_seconds() / 3600

        if round(diff, 2) <= 0.01:
            diff_minutes = (closed_at - created_at).total_seconds() / 60
            print(f"{task['title']} : {diff_minutes} minutes")
        else:
            print(f"{task['title']} : {round(diff, 2)} heures")


def get_tasks_by_column_id(id):
    tasks = kanban_api.get_request(f"https://api.github.com/projects/columns/{id}/cards").json()
    return tasks


def show_total_tasks_number(tasks):
    print(f"Nombre total de taches : {len(tasks)} tache")


def check_is_digit(input_str):
    if input_str.strip().isdigit():
        return True
    else:
        return False
