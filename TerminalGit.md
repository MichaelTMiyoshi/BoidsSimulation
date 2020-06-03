# Using Git from the Terminal in MacOS

It seems that using Terminal and the command line Git is the way to go when making Unity projects with a GitHub repo.  I made the folder for the repo, then followed instructions on how to make a [local Git repo](https://guides.codepath.com/ios/Using-Git-with-Terminal).  Then, I followed [pull instructions](https://www.atlassian.com/git/tutorials/syncing/git-pull) to get the repository I had already made down to the local machine.  Then, I made the project.  Just an empty project at first.  Then, I pushed the project.  I followed the [local Git repo](https://guides.codepath.com/ios/Using-Git-with-Terminal) instructions when I pushed the project.

Here is what I came up with in terms of process.
1. Make the repo folder wherever you like to make your projects using either Finder or Terminal.  (I decided that I wanted to put the folder in the UnityProjects folder.  But I wanted to make sure the project was a folder deeper.)
1. You can go ahead and add the readme.md, license, .gitattributes, and .gitignore files if you like.  (Probably a good thing to do.)  You can use the sample .gitattributes and .gitignore files from [thoughtbot](https://thoughtbot.com/blog/how-to-git-with-unity).  Another good source for creating [.gitignore](https://www.gitignore.io) is the link (which is `https://www.gitignore.io`).
1. Using Terminal:
   1. Navigate to the repo folder.  cd foldername
   1. git init
   1. git remote add origin `https://github.com/UserName/repo.git` (https) or
   1. ~~git remote add origin `git@github.com:UserName/repo.git` (SSH)~~  (You can use it, but I wanted to avoid confusion.  Note: You can only use one or the other, https or SSH.)
   1. git pull repo.git (use the full url including https://)
1. Create the Unity project in your repo folder.  This will create a new folder instead of making the project right at the front of your repo.
1. Using Terminal:
   1. git status (to see what files need to be included in the commit)
   1. git add . (adds all the files with changes)
   1. git status (optional in case you want to see what files will be included in the commit)
   1. git commit -m "Commit message"
   1. git push origin master

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

The process is pretty simple once you do it.  I used SSH first, but then figured out I needed to create an SSH key.  So I had to change the type of remote I was using.  I found a nice article to help me get the [remote change](https://help.github.jp/enterprise/2.11/user/articles/changing-a-remote-s-url/) done.

Using terminal:
1. git remote -v
1. git remote set-url origin `https://hostname/USERNAME/repository.git`
1. git remote -v (lists the remotes and verifies that they changed)

After doing this the first time, I think it is probably the way to start projects.  I am not sure that it is the way to keep projects up to date.  I need to add GitHub for Unity into the project to see if it will actually do what I want it to do.
