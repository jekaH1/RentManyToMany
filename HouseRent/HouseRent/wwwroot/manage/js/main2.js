let deletebtn = document.querySelectorAll(".delete-image-apartment")

deletebtn.forEach(btn => btn.addEventListener("click", function (e) {
    
    btn.parentElement.remove()
    
}))
