function vote(postId, direction) {
    var url = '/Home/Vote/';
    url += postId + '?direction=';
    url += (direction > 0) ? 'p' : 'n';

    var xhr = new XMLHttpRequest();
    xhr.open('GET', url);
    xhr.responseType = 'text';
    xhr.onreadystatechange = function () {
        var voteCounter = document.querySelector('#post-score-' + postId);
        voteCounter.className += (direction > 0) ? ' voted-up' : ' voted-down';
        voteCounter.innerHTML = xhr.response;
    };

    xhr.send();
}