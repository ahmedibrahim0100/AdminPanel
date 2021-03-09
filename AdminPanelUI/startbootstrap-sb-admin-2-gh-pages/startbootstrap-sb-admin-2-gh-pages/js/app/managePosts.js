import { environment } from './environment.js'

let addPostDiv = document.getElementById('addPostDiv');
let userSelectedImagesList = document.getElementById('userSelectedImagesList');
let selectedImages = [];
let categoriesList = document.getElementById('categoriesList');
let authorsList = document.getElementById('authorsList');

document.getElementById('addPostButton').addEventListener('click', viewAddPostForm);
document.getElementById('uploadImageButton').addEventListener('click', openDialogClick);
document.getElementById('addPostForm').addEventListener('submit', postArticle);

fetch(environment.apiURL+'/NewsCategoryModels')
    .then((res) => res.json())
    .then((categories) => {
        categories.forEach(category => categoriesList.options.add(new Option(category.Name, category.Id)));        
    });

fetch(environment.apiURL + '/AuthorModels')
    .then((res) => res.json())
    .then((authors) => {
        authors.forEach(author => authorsList.options.add(new Option(author.Name, author.Id)));
    })


function viewAddPostForm() {
    if (addPostDiv.style.display === 'none') {
        addPostDiv.style.display = '';
    }
}

function openDialogClick() {
    openfileDialog('.png, .jpg, .jpeg', true, fileDialogChanged);
}

function openfileDialog(accept, multy = false, callback) {
    var inputElement = document.createElement('input');
    inputElement.type = 'file';
    inputElement.setAttribute('id', 'selectImages');
    inputElement.accept = accept;
    if(multy){
        inputElement.multiple = multy;
    }

    if(typeof callback === 'function'){
        inputElement.addEventListener('change', callback);
    }

    inputElement.dispatchEvent(new MouseEvent('click'));
}

// function fileDialogChanged(event) {
//     [...this.files].forEach(img => {
//         var idxDot = img.name.lastIndexOf(".") + 1;
//         var extFile = img.name.substr(idxDot, img.name.length).toLowerCase();
//         if (extFile=="jpg" || extFile=="jpeg" || extFile=="png"){
//                 let formData = new FormData();
//                 formData.append("img", img);
//                 fetch(environment.apiURL +'/uploadimage', {
//                     method: 'POST', 
//                     body: formData,
//                     mode: 'no-cors'
//                 })
//                 .then(res => {
//                         var li = document.createElement('li');
//                         li.appendChild(document.createTextNode(`${img.name}`));
//                         userSelectedImagesList.appendChild(li);
//                 },
//                 err => {
//                     console.log(err);
//                     alert('Error during uploading. Try again !!');
//                 })           
//         }else{
//             alert("Only jpg/jpeg and png files are allowed!");
//         }         
//     }

//     )
// }

function fileDialogChanged(event){
    [...this.files].forEach(img => {
        var idxDot = img.name.lastIndexOf(".") + 1;
        var extFile = img.name.substr(idxDot, img.name.length).toLowerCase();
        if (extFile=="jpg" || extFile=="jpeg" || extFile=="png"){
            selectedImages.push(img);
            var li = document.createElement('li');
            li.appendChild(document.createTextNode(`${img.name}`));
            userSelectedImagesList.appendChild(li);
        }else{
            alert("Only jpg/jpeg and png files are allowed!");
        }  
    }
    )
}

function postArticle(e) {
    e.preventDefault();

    let postedArticle = {
        AuthorId: authorsList.value,
        CategoryId: categoriesList.value,
        Title: document.getElementById('title').value,
        Headline: document.getElementById('headline').value,
        Body: document.getElementById('body').value
    };

    let formData = new FormData();
    formData.append("postedArticle", JSON.stringify(postedArticle));
    formData.append("images", selectedImages);

    fetch(environment.apiURL + '/postarticle', {
        method: 'POST',
        header: {
            'Accept': 'application/json',
            'Content-Type': 'multipart/form-data',
          },
        body: formData,
        mode: 'no-cors'
    })
    .then(res => res.json())
    .catch(err => console.log(err));

}


