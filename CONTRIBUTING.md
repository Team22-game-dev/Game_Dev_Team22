# Prerequisites:
- Git is installed and fully configured with your GitHub account on your local machine
- Unity Hub and Unity Editor `2022.3.55f1` is installed

# Contributing Guide:

## Set up
- Navigate to the desired directory, then run `git clone https://github.com/Team22-game-dev/Game_Dev_Team22.git` (or equivalent)
- Open Unity Hub, go to the `Projects` tab, click on `Add`, click on `Add project from disk`, navigate to the directory `Game_Dev_Team22` that you just cloned, and click on `Open`

## Creating a Pull Request
- Navigate to [Issues on Github](https://github.com/Team22-game-dev/Game_Dev_Team22/issues) and click on a desired issue, click `Create a branch for this issue or link a pull request.`, click `Checkout locally`, click `Create branch`, taking note of the branch name (e.g. `22-set-up-repo`)
- Navigate to the directory `Game_Dev_Team22`, then run `git fetch` (or equivalent)
- Run `git checkout <branch name>` (or equivalent) (e.g. `git checkout 22-set-up-repo`)
- Create the relevant changes on the Unity Editor
- Run `git pull origin main` (or equivalent) to synchronize changes from the remote repository to your local machine
- Run `git status` (or equivalent) to see all changed files that have not been committed
- Run `git add .` (or equivalent) to stage all changed files or `git add <file name>` (or equivalent) to stage a specific file
- Run `git commit -m "Your commit message here"` (or equivalent) to commit changes locally
- Run `git push` (or equivalent) to push commits to the remote repository
- Navigate to [Pull Requests on GitHub](https://github.com/Team22-game-dev/Game_Dev_Team22/pulls), click on `New pull request`, click on the branch you worked on, click `Create pull request`, update the title, description, reviewers, and assignees as desired, then click `Create pull request`