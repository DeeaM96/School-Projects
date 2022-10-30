import sys
import requests
import datetime
import old.integration_continue_metric
import old.pull_request_metric
import old.persistence_metric
import old.kanban_metric

username = "DavidOlivierBouchard"
token = "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"


def main():
    global org_name
    global repo_name
    global owner_name
    global project_id

    org_name = input("Entrer le nom de l'organisation GitHub : ")
    repo_name = input("Entrer le nom du repository GitHub : ")

    owner_name = select_repo()
    project_id = select_project()

    print("""
    Oxygène Software - Métriques DevOps

    Entrer '1' pour métriques Kanban
    Entrer '2' pour métriques "pull-request" 
    Entrer '3' pour métrique nécessitant une couche de persistance
    Entrer '4' pour métrique d'integration continue
    """)

    choice = int(input("Entrer choix : "))
    if choice == 1:
        kanban_menu()
    elif choice == 2:
        pull_request_menu()
    elif choice == 3:
        persistence_menu()
    elif choice == 4:
        integration_continue_menu()
    else:
        print("choix invalide, fermeture du programme")
        sys.exit()


def select_repo():
    i = 0
    repo_found = False
    while not repo_found:
        res = requests.get(f"https://api.github.com/orgs/{org_name}/repos?page={i}", auth=(username, token))
        repos = res.json()
        for repo in repos:
            if (repo['name'] == repo_name):
                repo_found = True
                owner = repo['owner']['login']
                return owner
        i += 1


def select_project():
    res = requests.get(f"https://api.github.com/repos/{owner_name}/{repo_name}/projects", auth=(username, token))
    projects = res.json()

    for project in projects:
        print("Ouvrir le projet '" + project['name'] + "' ?")
        choice = input("y/n : ")
        if choice == "y":
            return project['id']


def kanban_menu():
    print("""
    Métriques “Kanban”
    
    Entrer '1' pour afficher le temps (lead time) pour une tâche donnée
    Entrer '2' pour afficher le temps (lead time) pour les tâches terminées dans une période de temps donnée
    Entrer '3' pour afficher le nombre de tâches actives pour une colonne donnée
    Entrer '4' pour afficher le nombre de tâches complètes pour une période de temps donnée
    Entrer '0' pour retour en arrière'
    """)
    choice = int(input("Entrer choix : "))
    if choice == 1:
        print("Voici la liste des tâches")
        tasks = old.kanban_metric.get_all_issues()
        old.kanban_metric.print_tasks(tasks)
        selected_task = input("Entrez le numéro de la tâche pour voir son temps (0 pour retour en arrière): ")
        if int(selected_task) == 0:
            kanban_menu()
        else:
            old.kanban_metric.select_task_for_time(selected_task, tasks)

        continue_kanban()
    elif choice == 2:
        start_date = input("Entrez la date de debut de la periode (AAAA-MM-JJ): ")
        while not correct_date_format(start_date):
            print("Le format de la date n'est pas correct, veuillez reesayer")
            start_date = input("Entrez la date de debut de la periode (AAAA-MM-JJ): ")
        end_date = input("Entrez la date de fin de la periode(AAAA-MM-JJ): ")
        while not correct_date_format(end_date):
            print("Le format de la date n'est pas correct, veuillez reesayer")
            end_date = input("Entrez la date de debut de la periode (AAAA-MM-JJ): ")
        tasks = old.kanban_metric.get_done_issues();
        tasks_between_dates = old.kanban_metric.get_tasks_in_between_dates(start_date, end_date, tasks)

        old.kanban_metric.show_times_for_tasks(tasks_between_dates)

        continue_kanban()
    elif choice == 3:
        print("""
            Entrer '1' pour afficher le nombre des issues actives dans la colonne "Backlog"
            Entrer '2' pour afficher le nombre des issues actives dans la colonne "To do"
            Entrer '3' pour afficher le nombre des issues actives dans la colonne "In progress"
            Entrer '4' pour afficher le nombre des issues actives dans la colonne "To be reviewed"
            Entrer '5' pour afficher le nombre des issues actives dans la colonne "Done"
            Entrer '0' pour retour en arriere'
            """)

        choice_column = int(input("Entrer choix : "))
        if choice_column == 1:
            tasks = old.kanban_metric.get_tasks_by_column_id(18671748)
            old.kanban_metric.show_total_tasks_number(tasks)
        elif choice_column == 2:
            tasks = old.kanban_metric.get_tasks_by_column_id(18671752)
            old.kanban_metric.show_total_tasks_number(tasks)
        elif choice_column == 3:
            tasks = old.kanban_metric.get_tasks_by_column_id(18671753)
            old.kanban_metric.show_total_tasks_number(tasks)
        elif choice_column == 4:
            tasks = old.kanban_metric.get_tasks_by_column_id(18671754)
            old.kanban_metric.show_total_tasks_number(tasks)
        elif choice_column == 5:
            tasks = old.kanban_metric.get_tasks_by_column_id(18671754)
            old.kanban_metric.show_total_tasks_number(tasks)
        elif choice_column == 0:
            kanban_menu()
        else:
            print("choix invalide, fermeture du programme")

        continue_kanban()
    elif choice == 4:
        tasks = old.kanban_metric.get_done_issues()
        start_date = input("Entrez la date de debut de la periode (AAAA-MM-JJ): ")
        while not correct_date_format(start_date):
            print("Le format de la date n'est pas correct, veuillez reesayer")
            start_date = input("Entrez la date de debut de la periode (AAAA-MM-JJ): ")
        end_date = input("Entrez la date de fin de la periode(AAAA-MM-JJ): ")
        while not correct_date_format(end_date):
            print("Le format de la date n'est pas correct, veuillez reesayer")
            end_date = input("Entrez la date de debut de la periode (AAAA-MM-JJ): ")
        tasks_between_dates = old.kanban_metric.get_tasks_in_between_dates(start_date, end_date, tasks)
        old.kanban_metric.show_total_tasks_number(tasks_between_dates)

        continue_kanban()
    elif choice == 0:
        main()
    else:
        print("Choix invalide, fermeture du programme")
        sys.exit()


def correct_date_format(date_string):
    format = "%Y-%m-%d"
    try:
        datetime.datetime.strptime(date_string, format)
        return True
    except ValueError:
        return False


def pull_request_menu():
    print("""
    Métriques “pull-request”

    Entrer '1' pour afficher le nombre total de 'pull requests'
    Entrer '2' pour afficher le temps moyen des 'pull requests'
    Entrer '3' pour afficher le temps d'un 'pull request'
    Entrer '4' pour afficher le nombre total de 'reviews'
    Entrer '5' pour afficher le nombre des 'reviews' pour un 'pull request'
    Entrer '0' pour retour en arriere'
    """)
    choice = int(input("Entrer choix : "))
    if choice == 1:
        old.pull_request_metric.total_pr(org_name, repo_name);
        continue_pull_request()
    elif choice == 2:
        old.pull_request_metric.average_time_all_pr(org_name, repo_name)
        continue_pull_request()
    elif choice == 3:
        old.pull_request_metric.menu_closed_prs(org_name, repo_name);
        selected_closed_pr = input("Entrez le numero du pull_request pour voir son temps (0 pour retour en arriere): ")

        if int(selected_closed_pr) == 0:
            pull_request_menu()
        else:
            old.pull_request_metric.select_closed_pr_for_time(org_name, repo_name, selected_closed_pr);

        continue_pull_request()
    elif choice == 4:
        old.pull_request_metric.total_reviews(org_name, repo_name)
        continue_pull_request()
    elif choice == 5:
        old.pull_request_metric.menu_all_prs(org_name, repo_name)
        selected_pr = input(
            "Entrez le numero du pull_request pour voir le nombre de reviews (0 pour retour en arriere): ")

        if int(selected_pr) == 0:
            pull_request_menu()
        else:
            old.pull_request_metric.select_pr_for_reviews(org_name, repo_name, selected_pr);
        continue_pull_request()
    elif choice == 0:
        main()
    else:
        print("choix invalide, fermeture du programme")
        sys.exit()


def continue_pull_request():
    inputKey = input("Appuyez sur Enter pour continuer: ")
    enter = False
    while not enter:
        if inputKey == "":
            enter = True
            pull_request_menu()
        else:
            inputKey = input("Appuyez sur Enter pour continuer: ")


def continue_kanban():
    inputKey = input("Appuyez sur Enter pour continuer: ")
    enter = False
    while not enter:
        if inputKey == "":
            enter = True
            kanban_menu()
        else:
            inputKey = input("Appuyez sur Enter pour continuer: ")


def persistence_menu():
    print(""" Menu Persistance
    Menu métrique persistance !

    Entrer '1' pour synchroniser la base de données
    Entrer '2' pour afficher la métrique persistance
    Entrer '0' pour retour en arriere'
    """)

    choice = int(input("Entrer choix : "))

    if choice == 1:
        old.persistence_metric.syncDatabase(project_id)
        continue_persistence()
    elif choice == 2:
        old.persistence_metric.menuMetric(project_id)
        continue_persistence()
    elif choice == 0:
        main()
    else:
        print("Choix invalide")
        persistence_menu()


def continue_persistence():
    inputKey = input("Appuyez sur Enter pour continuer: ")
    enter = False
    while not enter:
        if inputKey == "":
            enter = True
            persistence_menu()
            break
        else:
            inputKey = input("Appuyez sur Enter pour continuer: ")


def integration_continue_menu():
    print("""
    Métriques d'integration continue

    Entrer '1' pour afficher le temps d’exécution du pipeline de build pour un build donné
    Entrer '2' pour afficher le temps d'exécution des tests automatises pour un run donné
    Entrer '3' pour afficher quantité de runs pour un workflow
    Entrer '4' pour afficher quantité de tests automatisés réussis et échoués (tous les worflows)
    Entrer '0' pour retour en arrière'
    """)
    choice = int(input("Entrer choix : "))
    workflows = old.integration_continue_metric.get_all_workflows()

    if choice == 1:
        print("Choissisez un workflow : ")
        old.integration_continue_metric.print_workflows(workflows)
        selected_workflow = input(
            "Entrez le numéro du workflow pour voir la liste des runs (0 pour retour en arrière): ")
        if int(selected_workflow) == 0:
            integration_continue_menu()
        else:
            runs = old.integration_continue_metric.get_runs_for_workflow(selected_workflow, workflows)
            print("Choissisez un run : ")
            old.integration_continue_metric.print_runs(runs)
            selected_run = input("Entrez le numéro du run pour afficher le temps d'execution du build: ")
            if int(selected_run) == 0:
                integration_continue_menu()
            else:
                run = old.integration_continue_metric.get_run(selected_run, runs)
                old.integration_continue_metric.print_build_time(run)
        continue_integration_continue()
    elif choice == 2:
        print("Choissisez un workflow : ")
        old.integration_continue_metric.print_workflows(workflows)
        selected_workflow = input(
            "Entrez le numéro du workflow pour voir la liste des runs (0 pour retour en arrière): ")
        if int(selected_workflow) == 0:
            integration_continue_menu()
        else:
            runs = old.integration_continue_metric.get_runs_for_workflow(selected_workflow, workflows)
            print("Choissisez un run : ")
            old.integration_continue_metric.print_runs(runs)
            selected_run = input("Entrez le numéro du run pour afficher le temps d'execution du build: ")
            if int(selected_run) == 0:
                integration_continue_menu()
            else:
                run = old.integration_continue_metric.get_run(selected_run, runs)
                old.integration_continue_metric.print_test_time(run)

        continue_integration_continue()
    elif choice == 3:
        print("Choissisez un workflow : ")
        old.integration_continue_metric.print_workflows(workflows)
        selected_workflow = input(
            "Entrez le numéro du workflow pour voir la liste des runs (0 pour retour en arrière): ")
        if int(selected_workflow) == 0:
            integration_continue_menu()
        else:
            runs = old.integration_continue_metric.get_runs_for_workflow_count(selected_workflow, workflows)
            print(f"Cet workflow contient {runs} runs")

        continue_integration_continue()

    elif choice == 4:
        old.integration_continue_metric.count_tests(workflows)
        continue_integration_continue()
    elif choice == 0:
        main()
    else:
        print("Choix invalide, fermeture du programme")
        sys.exit()


def continue_integration_continue():
    inputKey = input("Appuyez sur Enter pour continuer: ")
    enter = False
    while not enter:
        if inputKey == "":
            enter = True
            integration_continue_menu()
        else:
            inputKey = input("Appuyez sur Enter pour continuer: ")
