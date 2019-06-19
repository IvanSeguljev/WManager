
$(window).on("load", function () {
    onemoguciPolja();
    
});
function onemoguciPolja() {
    var disabled = document.getElementsByClassName("shouldCheckFirst");

    for (d = 0; d < disabled.length; d++) {
        disabled[d].value = "";
        disabled[d].setAttribute("disabled", "true");
        
    }
}
function omoguciPolja() {
    var disabled = document.getElementsByClassName("shouldCheckFirst");

    for (d = 0; d < disabled.length; d++) {
        disabled[d].removeAttribute("disabled");
    }
    
}
function porukaUspeh(poruka) {
    Lobibox.notify('success', {
        pauseDelayOnHover: true,
        continueDelayOnInactiveTab: false,
        msg: poruka,
        title: 'Uspeh'
    });
}
function porukaNeuspeh(poruka) {
    Lobibox.notify('error', {
        pauseDelayOnHover: true,
        continueDelayOnInactiveTab: false,
        msg: poruka,
        title: 'Greška'
    });
}