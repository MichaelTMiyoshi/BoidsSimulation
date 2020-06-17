# Using Git from the Terminal (in MacOS)

It seems that using Terminal and the command line Git is the way to go when making Unity projects with a GitHub repo.  (It is the same on Mac and PC and Linux, but they use different tools.)  I made the folder for the repo, then followed instructions on how to make a [local Git repo](https://guides.codepath.com/ios/Using-Git-with-Terminal).  Then, I followed [pull instructions](https://www.atlassian.com/git/tutorials/syncing/git-pull) to get the repository I had already made down to the local machine.  Then, I made the project.  Just an empty project at first.  Then, I pushed the project.  I followed the [local Git repo](https://guides.codepath.com/ios/Using-Git-with-Terminal) instructions when I pushed the project.

## Creating Unity Project Repo

Here is what I came up with in terms of process.
1. Make the repo folder wherever you like to make your projects using either Finder or Terminal.  (I decided that I wanted to put the folder in the UnityProjects folder.  But I wanted to make sure the project was a folder deeper.)
1. You can go ahead and add the readme.md, license, .gitattributes, and .gitignore files if you like.  (Probably a good thing to do.)  You can use the sample .gitattributes and .gitignore files from [thoughtbot](https://thoughtbot.com/blog/how-to-git-with-unity).  Another good source for creating [.gitignore](https://www.gitignore.io) is the link (which is `https://www.gitignore.io`).
1. Using Terminal:
   1. Navigate to the repo folder.  cd foldername
   1. git init
   1. git remote add origin `https://github.com/UserName/repo.git` (https) or
   1. ~~git remote add origin `git@github.com:UserName/repo.git` (SSH)~~  (You can use it, but I wanted to avoid confusion.  Note: You can only use one or the other, https or SSH.)
   1. git pull `https://github.com/UserName/repo.git` (use the full url including https://)
1. Create the Unity project in your repo folder.  This will create a new folder instead of making the project right at the front of your repo.
1. Using Terminal:
   1. git status (to see what files need to be included in the commit)
   1. git add . (adds all the files with changes)
   1. git status (optional in case you want to see what files will be included in the commit)
   1. git commit -m "Commit message"
   1. git push origin master

### Pulling from the Repo

You might also need to pull the project from the GitHub repo before pushing.  This can happen when you change files on GitHub rather than in the local repo on your computer (like editing a readme.md or other .md file or any online file for that matter).  You just need to pull the repo before pushing.  If you already know you need to pull first, you can just pull it at the top of your process.

1. Using Terminal:
   1. git pull `https://github.com/UserName/repo.git` master
   1. git status (to see what files need to be included in the commit)
   1. git add . (adds all the files with changes)
   1. git status (optional in case you want to see what files will be included in the commit)
   1. git commit -m "Commit message"
   1. git push origin master
   1. (or you might need to pull here after getting an error message) git pull `https://github.com/UserName/repo.git` master
   1. git push origin master

### Changing from SSH to HTTPS

The process is pretty simple once you do it.  I started using SSH first, but then figured out I needed to create an SSH key.  (Which I did not do.)  So I changed the type of remote I was using.  I found a nice article to help me get the [remote change](https://help.github.jp/enterprise/2.11/user/articles/changing-a-remote-s-url/) done.

Using terminal:
1. git remote -v
1. git remote set-url origin `https://hostname/USERNAME/repository.git`
1. git remote -v (lists the remotes and verifies that they changed)

### Creating a Branch

I used Git to create my first branch ever.  This link on [how to](https://www.datree.io/resources/git-create-branch) does a nice job of explaining not just how to create a branch, but why you use the command you do.

Using terminal:
1. git pull (if you need to make sure your project is up to date)
1. git status (just to make sure you are up to date)
1. git checkout -b `branchname` (-b makes a new branch, `branchname` is the name of your branch, this command also puts you in the new branch)
1. git status (just to make sure you are in the correct branch)
1. git push --set-upstream origin flocking (original push of your branch)

After you do some updates and so forth, you can use the process above for pushing updates.  Just push to the branch instead of master.  (git push origin `branchname`)

### Merging a Branch

I did my first merge of the flocking branch back into the master branch.  Followed this [merge how to](https://stackabuse.com/git-merge-branch-into-master/) to get it done.

Using terminal:
1. git checkout master (goes to the master branch)
1. git merge `branchname` (merges the branch back into master)
1. git push origin master

You might need to pull your master if your push does not work.  I had to do that.  But you just pull from the remote and then push.

## Other Thoughts

Note: I have not yet done a merge back to the master.  I think that will come later and I will document that here as well.

After doing this (setting up a Unity project on GitHub) the first time, I think it is probably the way to start projects.  I am not sure that it is the way to keep projects up to date.  I need to add GitHub for Unity into the project to see if it will actually do what I want it to do.

I took the GitHub add-in out of my Unity project.  It was not what I needed it to be.  Either that or I did not know how it worked.  Either way, using Git from the terminal is easy and the way I am going to manage Unity projects on GitHub.  The other note is that you need to install the plugin on each project rather than on Unity itself.  Not what I imagined.  Not that convenient.  And it does not work the way I expected.  Git and GitHub work nicely together.  I will continue to use Git on the terminal.

Note: This document is not chronological.  It is supposed to be logical.  Might move the SSH versus HTTPS stuff later in the process.
