from old.api import integration_continue_api
import datetime


def get_all_workflows():
    workflows = integration_continue_api.get_request(
        f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/workflows").json()
    return workflows['workflows']


def get_run(selected_run, runs):
    total_runs = len(runs)
    if check_is_digit(selected_run):
        if total_runs > int(selected_run) - 1 >= 0:
            selected_run = runs[int(int(selected_run) - 1)]
            run = integration_continue_api.get_request(
                f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/runs/{selected_run['id']}").json()
            return run
        else:
            print("choix invalide")
    else:
        print("choix invalide")


def get_runs_for_workflow_count(selected_workflow, workflows):
    total_workflows = len(workflows)
    if check_is_digit(selected_workflow):
        if total_workflows > int(selected_workflow) - 1 >= 0:
            selected_workflow = workflows[int(int(selected_workflow) - 1)]
            runs = integration_continue_api.get_request(
                f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/workflows/{selected_workflow['id']}/runs").json()
            return runs['total_count']
        else:
            print("choix invalide")
    else:
        print("choix invalide")

def get_runs_for_workflow(selected_workflow, workflows):
    total_workflows = len(workflows)
    if check_is_digit(selected_workflow):
        if total_workflows > int(selected_workflow) - 1 >= 0:
            selected_workflow = workflows[int(int(selected_workflow) - 1)]
            runs = integration_continue_api.get_request(
                f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/workflows/{selected_workflow['id']}/runs").json()
            return runs['workflow_runs']
        else:
            print("choix invalide")
    else:
        print("choix invalide")


def print_workflows(workflows):
    i = 1
    for workflow in workflows:
        print(f"{i}. {workflow['name']}")
        i += 1


def print_runs(runs):
    i = 1
    for run in runs:
        head_commit = integration_continue_api.get_request(
            f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/runs/{run['id']}").json()[
            'head_commit']['message']
        name = integration_continue_api.get_request(
            f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/runs/{run['id']}").json()['name']
        run_number = integration_continue_api.get_request(
            f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/runs/{run['id']}").json()['run_number']
        print(f"{i}.{repr(head_commit)} {name} #{run_number}")
        i += 1


def print_build_time(run):
    jobs = integration_continue_api.get_request(f"{run['jobs_url']}").json()['jobs']
    for job in jobs:
        if job['name'].find('build') != -1:
            if job['conclusion'] != 'skipped':
                created_at = datetime.datetime.strptime(job["started_at"], '%Y-%m-%dT%H:%M:%SZ')
                closed_at = datetime.datetime.strptime(job["completed_at"], '%Y-%m-%dT%H:%M:%SZ')

                diff = (closed_at - created_at).total_seconds() / 3600

                if round(diff, 2) <= 0.01:
                    diff_minutes = (closed_at - created_at).total_seconds() / 60
                    print(f"Le temps de ce build est de : {diff_minutes} minutes")
                else:
                    print(f"Le temps de ce build est de : {round(diff, 2)} heures")
                break
            else:
                print("Ce run n'a pas execute un build")


def print_test_time(run):
    jobs = integration_continue_api.get_request(f"{run['jobs_url']}").json()['jobs']
    for job in jobs:
        if job['name'].find('test') != -1:
            if job['status'] == 'completed':
                created_at = datetime.datetime.strptime(job["started_at"], '%Y-%m-%dT%H:%M:%SZ')
                closed_at = datetime.datetime.strptime(job["completed_at"], '%Y-%m-%dT%H:%M:%SZ')

                diff = (closed_at - created_at).total_seconds() / 3600

                if round(diff, 2) <= 0.01:
                    diff_minutes = (closed_at - created_at).total_seconds() / 60
                    print(
                        f"Les tests ont ete execute dans: {diff_minutes} minutes avec le status de {job['conclusion']}")
                else:
                    print(
                        f"Les tests ont ete execute dans : {round(diff, 2)} heures  avec le status de {job['conclusion']}")
                break;
            else:
                print("Cet run n'a pas fini les tests")
        else:
            print("Cet run n'a pas execute des tests")


def check_is_digit(input_str):
    if input_str.strip().isdigit():
        return True
    else:
        return False


def count_tests(workflows):
    success_count = 0
    failure_count = 0
    for workflow in workflows:
        summary_runs = integration_continue_api.get_request(
            f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/workflows/{workflow['id']}/runs").json()[
            'workflow_runs']
        summary_runs += integration_continue_api.get_request(
            f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/workflows/{workflow['id']}/runs?page=2").json()[
            'workflow_runs']
        summary_runs += integration_continue_api.get_request(
            f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/workflows/{workflow['id']}/runs?page=3").json()[
            'workflow_runs']

        for summary_run in summary_runs:
            run = integration_continue_api.get_request(
                f"https://api.github.com/repos/log680-gr1-eq10/hvac-gr1-eq10/actions/runs/{summary_run['id']}").json()

            jobs = integration_continue_api.get_request(f"{run['jobs_url']}").json()['jobs']
            for job in jobs:
                if job['name'].find('test') != -1:
                    if job['status'] == 'completed':
                        if job['conclusion'] == "success":
                            success_count += 1
                        if job['conclusion'] == "failure":
                            failure_count += 1
                        break

    print(f"Nombre total des tests reussis: {success_count}")
    print(f"Nombre total des tests echoues: {failure_count}")
