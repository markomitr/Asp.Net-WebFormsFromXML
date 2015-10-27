var gridZaPrikazPodatoci = "";

function povikajAjax(id,podatoci) {

    gridZaPrikazPodatoci = "SearchGrid" + "_" + id.toUpperCase();

    var ajaxFunc = $("#" + id).attr("povikAJAX");

    if (ajaxFunc != null && ajaxFunc !="")
    {
        if (podatoci.length > 3) {

            podatok = JSON.stringify(podatoci);

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: "{'podatok':'" + podatok + "'}",
                url: "Funkcii.asmx/" + ajaxFunc,
                dataType: "json",
                success: function (msg) {
                    if (msg != null) {
                        prikaziPodatoci(msg.d, id);
                    }
                    else
                    // ne e vo red
                        prikaziInfo(dajmiporaka("ERRAJAX"));
                },
                beforeSend: function (data) {
                    //Pred pustanje
                },
                complete: function (data) {
                    //Zavrsivanje na povik
                },
                error: function (msg) {
                    //Greska
                    prikaziInfo(dajmiporaka("ERRAJAX") + msg.d);
                }

            });
        
        }
        else {

            //$("#SearchGrid").html("");
            $("#" + gridZaPrikazPodatoci).html("");

        }
    }
}

function prikaziPodatoci(podatoci, koePoleID) {

    var nizaPodatoci = JSON.parse(podatoci);

    if (nizaPodatoci.length == 0) {
        $("#" + gridZaPrikazPodatoci).html(dajmiporaka("NoData"))
        return;
    }
    //$("#SearchGrid").html("");
    $("#" + gridZaPrikazPodatoci).html("");
    for (i = 0; i < nizaPodatoci.length; i++) {

        var pomObjekt = new NasObj(nizaPodatoci[i]);
        var tekObjekt = pomObjekt.VratiObjekt();

        var vrednost = tekObjekt.ZemiVrednost();
        var ime = tekObjekt.ZemiIme();

        var kontrola = ""
        kontrola += "<div ";
        kontrola += "id='PrikazPodatoci' ";
        kontrola += 'onclick="dodeliVrednost(this)"';
        kontrola += "KoePoleID='" +koePoleID +"'";
        kontrola += "GridID='"+gridZaPrikazPodatoci+"'";
        kontrola += " >";

        kontrola += "<input type='hidden' koePoleID='" + koePoleID + "' vrednost='" + vrednost + "' ime='" + ime + "'/>";

        kontrola += "<span ";
        kontrola += "id='VrednostPrikaz' ";
        kontrola += "Vrednost='" + vrednost + "'";
        kontrola += " >";
        kontrola += "[" + vrednost + "]";
        kontrola += "</span>";

        kontrola += "<br/ >";

        kontrola += "<span id='ImePrikaz' ";
        kontrola += " Ime='" + ime + "' ";
        kontrola += " >";
        kontrola += ime;
        kontrola += "</span>";

        kontrola += "</div>";

        //$("#SearchGrid").append(kontrola);
        $("#" + gridZaPrikazPodatoci).append(kontrola);
    }
}

function dodeliVrednost(objekt) {
    //alert($('[nasId="tboxTest"]').val());

    // Za PRIKAZ NA IMETO OD IZBRANATA VREDNOST
    //$('[nasId="ImeArtIzbor"]').html($(pom).children("#ImeArtPrikaz").html());
    var idOdTxtPole = $(objekt).attr("KoePoleID")
    var IzbranObjektTekst = $(objekt).children("#VrednostPrikaz").attr("Vrednost") + " " + $(objekt).children("#ImePrikaz").attr("Ime")
    $('#Opis_' + idOdTxtPole.toUpperCase()).html(IzbranObjektTekst);
    $('#' + idOdTxtPole).val($(objekt).children('#VrednostPrikaz').attr("Vrednost"));
    gridZaPrikazPodatoci = $(objekt).attr("GridID");
    $("#" + gridZaPrikazPodatoci).html("");
}

function navigacijaMeni(jas) {
    window.location = $(jas).attr("imefile");
}