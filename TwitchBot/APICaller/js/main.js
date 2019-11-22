document.getElementById('retrieve-button').addEventListener('click', getClips);

function getClips(event) {
    let route = 'https://api.twitch.tv/helix/clips?broadcaster_id=56339941';
    fetch(route, {
        method: 'GET',
        headers: {
            Host: 'api.twitch.tv',
            Authentication: 'Bearer 4onmr2h2modqs0nm15179q6bmpo8jo',
            Accept: '*/*',
        }
            })
    .then((response) => {
        return response.json();
    })
    .then((data) => {
        displayClips(data);
    })
    .catch((err) => {
        console.log(err);
    })
}

function displayClips(data) {
    Array.from(data).forEach(clip => {
        console.log(clip.embed_url);
    });
}