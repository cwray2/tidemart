const baseUrl = "https://localhost:6969/api/users";
var userList = [];
var myUser = {};

function onSignIn() {
    myUser = {};
    var searchID = document.getElementById("typeUsername").value;
    var searchPass = document.getElementById("typePassword").value;

    console.log(searchID);
    console.log(searchPass);

    const allUsersApiUrl = baseUrl;
    fetch(allUsersApiUrl).then(function(response) {
        return response.json();
    }).then(function(json) {
        userList = json;
        json.forEach((user) => {
            console.log(user);
            //console.log(user);
            if(user.username == searchID && user.password == searchPass) {
                myUser = user;
                console.log(myUser);
                window.location.replace('../client/employee.html');
            }
        });

        //window.location.replace('../client/employee.html');

        if (myUser == null) {
            console.log("user is null");
        }
        
    }).catch(function(error) {
        console.log(error);
    });

}

function populateList() {
    const allSongsApiUrl = baseUrl;
    fetch(allSongsApiUrl).then(function(response) {
        return response.json();
    }).then(function(json) {
        songList = json;
        json.forEach((song) => {
            console.log(song.songTitle)
            
        });

        
    }).catch(function(error) {
        console.log(error);
    });
}