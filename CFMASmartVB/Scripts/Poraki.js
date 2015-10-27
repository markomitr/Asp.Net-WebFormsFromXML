var Drzava="EN"
var Poraki = new Array();
Poraki["ERRAJAX_MK"] = "Грешка при повик на АЈАХ ";
Poraki["NODATA_MK"] = "Нема податоци";
Poraki["NASLOVINFOBOX_MK"] = "ИНФО - Порака!";

/*PORAKI EN*/
Poraki["ERRAJAX_EN"] = "ERROR - Ajax Call ";
Poraki["NODATA_EN"] = "No DATA";
Poraki["NASLOVINFOBOX_EN"] = "INFO- Message!";


function dajmiporaka(kojaporaka) {
    kojaporaka = kojaporaka.toUpperCase();

    if (Poraki[kojaporaka + "_" + Drzava] != undefined) {
        return Poraki[kojaporaka + "_" + Drzava];
    }
    else {
        return "###";
    }
}
function prikaziInfo(poraka) {
    if ($("#dialog p").length) {
        $("#dialog p").html(poraka);
    }
    else {
        var pom = "<div id='dialog' title='" + dajmiporaka("NASLOVINFOBOX") + "'>";
        pom +="<p>";
        pom +=poraka;
        pom +="</p>";
        pom += "</div>";
        $("form").append(pom);
    }

    $("#dialog").dialog();

}