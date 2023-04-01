document.getElementById("all").onchange = () => {
    const ref = document.getElementById("all").checked;

    document.querySelectorAll(".form-check-input").forEach((item) => {
        item.checked = ref;
    })
};

$('.nav-link').click((e) => {
    $(".active").removeClass('active');
    $(e.target).addClass('active');
});