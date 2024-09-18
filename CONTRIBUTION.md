# CONTRIBUTION.md

## Main Branches:

### ```master``` — The branch for production-ready code. Only stable, fully-tested code is merged here.

### ```develop``` or ```dev``` — The branch for ongoing development. All new features and changes are integrated and tested here before being pushed to production.

## Supporting Branches:

### ```Feature Branches (feature/*)``` — Used to develop new features or make changes. Each feature has its own branch and is created from develop. 

To create: ```git checkout -b feature/clup-task-id_feature_name develop```

To merge: After completing the feature, merge it back into develop:

```git checkout develop```,

```git merge feature/clup-task-id_feature_name```.

Then, delete the feature branch:

```git branch -d feature/clup-task-id_feature_name```

Example: ```feature/clup-8695p0b0b_registration```

### ```Bugfix Branches (bugfix/*)``` — Used to fix bugs. Each bugfix has its own branch and is created from develop. 

To create: ```git checkout -b bugfix/clup-task-id_bugfix_name develop```

To merge: After completing the bug fixing, merge it back into develop:

```git checkout develop```,

```git merge bugfix/clup-task-id_bugfix_name```.

Then, delete the bugfix branch:

```git branch -d bugfix/clup-task-id_bugfix_name```

Example: ```bugfix/clup-8695p0b0b_registration```

### ```Hotfix Branches (hotfix/*)``` — Used for urgent bug fixes in production. Created from master when a critical issue needs to be resolved.

To create:

```git checkout -b hotfix/fix-name master```

To merge: After fixing the issue, merge it into both master and develop:

```git checkout master```, 

```git merge hotfix/fix-name```

```git checkout develop```,

```git merge hotfix/fix-name```

Then, delete the hotfix branch:

```git branch -d hotfix/fix-name```

## Workflow Process:

### Feature Development:
Developers create feature/* branches from develop, work on features, commit changes, push changes, create PR and after approve merge them back into develop.

### Bug Fixing:
Developers create bugfix/* branches from develop, work on bug, commit changes, push changes, create PR and after approve merge them back into develop.

### Hotfixes:
If a bug appears in production, a hotfix/* branch is created from master, the issue is fixed, commited, PR created, approved and the branch is merged into both master and develop.

This keeps the workflow focused and efficient while ensuring production stability.
