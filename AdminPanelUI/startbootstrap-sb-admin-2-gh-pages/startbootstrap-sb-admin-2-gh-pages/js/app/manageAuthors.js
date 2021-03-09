import { environment } from './environment.js'

let addAuthorDiv = document.getElementById('addAuthorDiv');
let authorsList = document.getElementById('authorsList');
let authorName = document.getElementById('authorName');
let authorsTableBody = document.getElementById('authorsTableBody');
let initialNameValue = 'default';
let newNameValue = 'default';
let addingAuthorResult = document.getElementById('addingAuthorResult');



document.getElementById('addAuthorButton').addEventListener('click', toggleViewAddAuthorForm);
document.getElementById('addAuthorForm').addEventListener('submit', addNewAuthor);
document.getElementById('editAuthorsButton').addEventListener('click', toggleAuthorsList);
//document.getElementById('editAuthorsButton').addEventListener('click', toggleVirtualList);
document.getElementById('authorsTable').addEventListener('click', deleteOrEditAuthor);




function addNewAuthor(e) {
    e.preventDefault();

    let author = document.getElementById('authorName').value;

    fetch(environment.apiURL+'/postauthor', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-type': 'application/json'
        },
        body: '"' + author + '"'
    }).then((res) => {
        if(res.ok){
            let data = res.json();
            data
            .then((d) => {
            addingAuthorResult.innerHTML = `${d.Name} has been successfully added`;
            authorName.value = '';
            })           
        }else {
            addingAuthorResult.innerHTML = 'Something went wrong !';
            authorName.value = '';
        }
    })
}


function toggleViewAddAuthorForm() {
    if (addAuthorDiv.style.display === 'none') {
        authorsList.style.display = 'none';
        authorName.value = '';
        addingAuthorResult.innerHTML = '';
        addAuthorDiv.style.display = '';
    }
}


function toggleAuthorsList() {
    authorsTableBody.innerHTML = '';
    fetch(environment.apiURL+'/AuthorModels')
    .then((res) => res.json())
    .then((authors) => {
        authors.forEach((author) => {
            let newRow = `
            <tr>
            <td>${author.Id}</td><td><input type="text" value="${author.Name}" readonly></td>
            <td><button id="editbtn${authors.indexOf(author)}" class="btn btn-primary mr-4">Edit</button>
            <button id="deletebtn${authors.indexOf(author)}" class="btn btn-danger mr-4">Delete</button></td>
            </tr>
            `;
            authorsTableBody.innerHTML += newRow;
            addBlurListeners()
            authorName.value = '';
            addAuthorDiv.style.display = 'none';
            authorsList.style.display = '';
        })
    });
}


function toggleVirtualList(e) {
    let authors = [
        {Id: 1, Name: 'ahmed'},
        {Id: 2, Name: 'Mohamed'},
        {Id: 3, Name: 'Azza'},
        {Id: 4, Name: 'Abeer'},
        {Id: 5, Name: 'Hala'},
        {Id: 6, Name: 'Reem'},
        {Id: 7, Name: 'Momen'},
        {Id: 8, Name: 'Sara'},
        {Id: 9, Name: 'Israa'}
    ];

    authors.forEach((author) => {
        let newRow = `
        <tr>
        <td>${author.Id}</td><td><input type="text" value="${author.Name}" readonly></td>
        <td><button id="editbtn${authors.indexOf(author)}" class="btn btn-primary mr-4">Edit</button>
        <button id="deletebtn${authors.indexOf(author)}" class="btn btn-danger mr-4">Delete</button></td>
        </tr>
        `;
        authorsTableBody.innerHTML += newRow;
        addBlurListeners()
        authorName.value = '';
        addAuthorDiv.style.display = 'none';
        authorsList.style.display = '';
    })
}


function deleteOrEditAuthor(e) {
    if(e.target.id.includes('deletebtn')){
        let authorIdValue = e.path[2].children[0].childNodes[0].nodeValue;
        if(confirm('Are you sure, deleting?')){
            fetch(environment.apiURL + '/deleteauthor', {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json, text/plain, */*',
                    'Content-type': 'application/json'
                },
                body: '"' + authorIdValue + '"'
            })
            .then((res) => {
                if(res.ok){
                    let r = e.target.parentElement.parentElement;
                    authorsTableBody.removeChild(r);
                }else { 
                    console.log(res.status);
                    alert(`Error: ${res.statusText}`);
                }
            });          
        }
    }else if(e.target.id.includes('editbtn')){
        let inputing = e.path[2].cells[1].children[0];
        inputing.removeAttribute('readonly');
        inputing.focus();
        initialNameValue = inputing.value;
    }
}


function operateNameChanges(e) { 
    if(e.target.parentElement.tagName.toLowerCase() == 'td'){
        newNameValue = e.target.value;
        if(newNameValue != initialNameValue){
            if(confirm('Are you sure about editing author data?')){
                let authorIdValue = e.path[2].cells[0].childNodes[0].nodeValue;
                               
                fetch(environment.apiURL + '/updateauthor', {
                    method: 'PUT',
                    headers: {
                        'Accept': 'application/json, text/plain, */*',
                        'Content-type': 'application/json'
                    },
                    body: JSON.stringify({ Id: authorIdValue, Name: newNameValue })
                }).then((res) => {
                    if(res.ok){
                        console.log(res.ok);
                    }else{ 
                        console.log(res.statusText);
                    }
                })

        }else{
            e.target.value = initialNameValue;
            newNameValue = initialNameValue;
            console.log(newNameValue);
        }
    }
    }
    e.target.setAttribute('readonly', 'readonly');   
}



function addBlurListeners() {
    let authorNamesInputFields = document.getElementsByTagName('input');
    for (let i = 0; i < authorNamesInputFields.length; i++) {
        authorNamesInputFields[i].addEventListener('change', operateNameChanges);
    }
}



