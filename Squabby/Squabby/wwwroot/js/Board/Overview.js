function LoadThreads(board, start) {
    getJson(`/b/${board}/getthreads?start=${start}`, function(err, data){
        let container = document.getElementById("threadcontainer");
        data.threads.forEach(function (thread) {
            container.appendChild(createElement(thread));
        });
    });
}

function createElement(thread) {
    let threadBox = document.createElement("div");
    let threadData = document.createElement("div");
    let threadInfo = document.createElement("p");
    let threadInfoBoard = document.createElement("a");
    let threadInfoUsername = document.createElement(thread.owner == null?"p":"a");
    let threadDate = document.createElement("p");
    let threadTitle = document.createElement("h1");
    let threadContent = document.createElement("p");
    let dislikeButton = document.createElement("div");
    let likeButton = document.createElement("div");

    threadBox.className = "box mb1r clickable";
    threadBox.onclick = function() { window.location = `/b/${thread.board}/${thread.id}` };
    threadData.className = "threaddata";
    threadInfo.className = "threadinfo";
    threadInfoBoard.textContent = `b/${thread.board}`;
    threadInfoBoard.href = `/b/${thread.board}`;
    threadInfoUsername.textContent = thread.owner || "Anonymous";
    threadInfoUsername.className = "di indent";
    if(thread.username != null) threadInfoUsername.href = `/u/${thread.owner}`;
    threadDate.className = "threaddate";
    threadDate.textContent = thread.creationDate;
    threadTitle.textContent = thread.title;
    threadContent.textContent = thread.content;
    likeButton.onclick = function(event) { postData(`/b/${thread.board}/like?boardId=${thread.boardId}&id=${thread.id}`); event.stopPropagation();};
    likeButton.className= "like dib fr";
    dislikeButton.onclick = function(event) { postData(`/b/${thread.board}/dislike?boardId=${thread.boardId}&id=${thread.id}`); event.stopPropagation(); };
    dislikeButton.className= "dislike dib fr";

    threadInfo.appendChild(threadInfoBoard);
    threadInfo.appendChild(threadInfoUsername);
    threadData.appendChild(threadInfo);
    threadData.appendChild(threadDate);
    threadBox.appendChild(threadData);
    threadBox.appendChild(threadTitle);
    threadBox.appendChild(threadContent);
    threadBox.appendChild(dislikeButton);
    threadBox.appendChild(likeButton);

    return threadBox;
}
