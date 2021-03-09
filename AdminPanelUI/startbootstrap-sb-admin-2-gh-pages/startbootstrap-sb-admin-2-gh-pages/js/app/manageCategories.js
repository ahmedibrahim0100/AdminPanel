//TODO - DONE - Modify api method of posting new categroy
//TODO - DONE - Modify route attribute to the previous method to be /api/postcategory
//TODO - DONE - Modify api method of getting categories list
//TODO - check its calling from the client side
//TODO - DONE - Modify api method of updating category entity
//TODO - DONE - Modify its route to /api//updatecategory
//TODO - DONE - Modify api method of deleting category
//TODO - DONE - modify its route to /api/deletecategory
//TODO - handle case of clicking edit categories button but no connection to server exists
//TODO - 

import { environment } from './environment.js'

let addCategoryDiv = document.getElementById('addCategoryDiv');
let categoriesList = document.getElementById('categoriesList');
let categoryName = document.getElementById('categoryName');
let categoriesTableBody = document.getElementById('categoriesTableBody');
let initialNameValue = 'default';
let newNameValue = 'default';
let addingCategoryResult = document.getElementById('addingCategoryResult');



document.getElementById('addCategoryButton').addEventListener('click', toggleViewAddCategoryForm);
document.getElementById('addCategoryForm').addEventListener('submit', addNewCategory);
document.getElementById('editCategoriesButton').addEventListener('click', toggleCategoriesList);
//document.getElementById('editCategoriessButton').addEventListener('click', toggleVirtualList);
document.getElementById('categoriesTable').addEventListener('click', deleteOrEditCategory);




function addNewCategory(e) {
    e.preventDefault();

    let category = document.getElementById('categoryName').value;

    fetch(environment.apiURL+'/postcategory', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-type': 'application/json'
        },
        body: '"' + category + '"'
    }).then((res) => {
        if(res.ok){
            let data = res.json();
            data
            .then((d) => {
            addingCategoryResult.innerHTML = `${d.Name} has been successfully added`;
            categoryName.value = '';
            })           
        }else {
            addingCategoryResult.innerHTML = 'Something went wrong !';
            categoryName.value = '';
        }
    })
}


function toggleViewAddCategoryForm() {
    if (addCategoryDiv.style.display === 'none') {
        categoriesList.style.display = 'none';
        categoryName.value = '';
        addingCategoryResult.innerHTML = '';
        addCategoryDiv.style.display = '';

    }
}


function toggleCategoriesList() {
    categoriesTableBody.innerHTML = '';
    fetch(environment.apiURL+'/NewsCategoryModels')
    .then((res) => res.json())
    .then((categories) => {
        categories.forEach((category) => {
            let newRow = `
            <tr>
            <td>${category.Id}</td><td><input type="text" value="${category.Name}" readonly></td>
            <td><button id="editbtn${categories.indexOf(category)}" class="btn btn-primary mr-4">Edit</button>
            <button id="deletebtn${categories.indexOf(category)}" class="btn btn-danger mr-4">Delete</button></td>
            </tr>
            `;
            categoriesTableBody.innerHTML += newRow;
            addBlurListeners()
            categoryName.value = '';
            addCategoryDiv.style.display = 'none';
            categoriesList.style.display = '';
        })
    });
}


// function toggleVirtualList(e) {
//     let authors = [
//         {Id: 1, Name: 'ahmed'},
//         {Id: 2, Name: 'Mohamed'},
//         {Id: 3, Name: 'Azza'},
//         {Id: 4, Name: 'Abeer'},
//         {Id: 5, Name: 'Hala'},
//         {Id: 6, Name: 'Reem'},
//         {Id: 7, Name: 'Momen'},
//         {Id: 8, Name: 'Sara'},
//         {Id: 9, Name: 'Israa'}
//     ];

//     authors.forEach((author) => {
//         let newRow = `
//         <tr>
//         <td>${author.Id}</td><td><input type="text" value="${author.Name}" readonly></td>
//         <td><button id="editbtn${authors.indexOf(author)}" class="btn btn-primary mr-4">Edit</button>
//         <button id="deletebtn${authors.indexOf(author)}" class="btn btn-danger mr-4">Delete</button></td>
//         </tr>
//         `;
//         authorsTableBody.innerHTML += newRow;
//         addBlurListeners()
//         authorName.value = '';
//         addAuthorDiv.style.display = 'none';
//         authorsList.style.display = '';
//     })
// }


function deleteOrEditCategory(e) {
    if(e.target.id.includes('deletebtn')){
        let categoryIdValue = e.path[2].children[0].childNodes[0].nodeValue;
        if(confirm('Are you sure, deleting?')){
            fetch(environment.apiURL + '/deletecategory', {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json, text/plain, */*',
                    'Content-type': 'application/json'
                },
                body: '"' + categoryIdValue + '"'
            })
            .then((res) => {
                if(res.ok){
                    let r = e.target.parentElement.parentElement;
                    categoriesTableBody.removeChild(r);
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
            if(confirm('Are you sure about editing category data?')){
                let categoryIdValue = e.path[2].cells[0].childNodes[0].nodeValue;
                               
                fetch(environment.apiURL + '/updatecategory', {
                    method: 'PUT',
                    headers: {
                        'Accept': 'application/json, text/plain, */*',
                        'Content-type': 'application/json'
                    },
                    body: JSON.stringify({ Id: categoryIdValue, Name: newNameValue })
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
    let categoriesNamesInputFields = document.getElementsByTagName('input');
    for (let i = 0; i < categoriesNamesInputFields.length; i++) {
        categoriesNamesInputFields[i].addEventListener('change', operateNameChanges);
    }
}



