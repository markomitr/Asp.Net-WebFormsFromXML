<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Izvestaj.aspx.vb" Inherits="CFMASmartVB.Izvestaj" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="Scripts/json2.js" type="text/javascript" ></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.2.min.js"></script>
    <script src="Scripts/css3-mediaqueries.js" type="text/javascript"></script>
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />

    <title>Тест Извештај</title>
<script type="text/javascript">

    $(document).ready(function () {

        $("div[Grupa]").children("#Sodrzina").hide();
        $("#Chck").val("test");

        $("div[Grupa]").each(function (index) {
            if ($(this).attr("Prikazi") == "DA") {

                $(this).children("#Sodrzina").show();
            }
        })

        $(".RadBtn").bind('click', function () {

            $(this).children("input[type='radio']").attr('checked', true);
        });

        $("#NaslovGrupa").bind('click', function () {


            if (!$(this).parent("div").children("#Sodrzina").is(':visible')) {
                $(this).parent("div").children("#Sodrzina").show();
            }
            else {
                $(this).parent("div").children("#Sodrzina").hide();
            }

        });
    });

    function ekran() {
        var elem = (document.compatMode === "CSS1Compat") ? document.documentElement : document.body;

        var height = elem.clientHeight;
        var width = elem.clientWidth;

        alert(width);
    }

    function povikajAjax(imeArt) {


        if (imeArt.length > 3) {

            imeArt = JSON.stringify(imeArt);

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: "{'imeArtikal':'" + imeArt + "'}",
                url: "Izvestaj.aspx/testzaajax",
                dataType: "json",
                success: function (msg) {
                    if (msg != null)
                        prikaziProdukti(msg.d);
                    else
                    // ne e vo red
                        alert("Greska");
                },
                beforeSend: function (data) {
                    //Pred pustanje
                },
                complete: function (data) {
                    //Zavrsivanje na povik
                },
                error: function (msg) {
                    //Greska
                    alert("Greska");
                }
            });
        }
        else {

            $("#SearchGrid").html("");
        }
    } // od Funkcia povikaj Ajax

    function prikaziProdukti(podatoci) {
        var nizaArtikli = JSON.parse(podatoci);
        $("#SearchGrid").html("");
        for (i = 0; i < nizaArtikli.length; i++) {

            $("#SearchGrid").append("<div id='ArtikalPrikaz' onclick='dodadiArtikal(this)'><span id='SifraArtPrikaz' SifraArt='" + nizaArtikli[i].SifraArtikal + "'>[" + nizaArtikli[i].SifraArtikal + "]</span> <br/ ><span id='ImeArtPrikaz'>" + nizaArtikli[i].ImeArtikal + "</span></div>");
        }
    }

    function dodadiArtikal(pom) {
        //alert($('[nasId="tboxTest"]').val());
        $('[nasId="ImeArtIzbor"]').html($(pom).children("#ImeArtPrikaz").html());
        $('[nasId="tboxTest"]').val($(pom).children("#SifraArtPrikaz").attr("SifraArt"));
        $("#SearchGrid").html("");
    }

    </script>
<style type="text/css">
.aspNetHidden
{
    display:none;
    border:none;
}
body
{
    background-color:Maroon;
    width:100%;
}
select
{
    border:2px Solid Gray;
}
#Content
{
        border:2px inset Gray ;
        margin:auto;
        width:90%;
        color:White;
        font-size:1.3em;
        padding:5px;
}
#OrgEd,#GrpOrgEd,#SifraArt
{
    border-bottom:2px Solid White;
    margin:3px;
    padding:3px;
}
#OrgEd
{
    width:99%;
}
#OrgEd select
{
    width:95%;
    margin:auto;
    height:40px;
    font-size:1.3em;
}

#GrpOrgEd
{
    width:99%;
}
#GrpOrgEd select
{
    width:95%;
    margin:auto;
    height:40px;
    font-size:1.3em;
        
}
#SifraArt
{
    width:99%;
    
}

#SifraArt input
{
    width:95%;
    height:40px;
    font-size:1.3em;
}

#kopcePrikazi
{
    margin-top:10px;
    text-align:center;
    
}
#kopcePrikazi input
{
    width:30%;
    height:40px;
    border:none;
    margin:auto;
    font-size:1.2em;
}
#SearchGrid
{
}
#ArtikalPrikaz
{
    width:95%;
    font-size:1.3em;
    border-bottom:1px solid Gray;
    text-align:left;
    background-color:White;
    color:Black;
    overflow:hidden;
    
}
    #SifraArtPrikaz
    {
        font-size:0.7em;
    }
#ArtikalPrikaz:hover
{
    cursor:pointer;
    border-bottom:2px inset Gray;
    font-weight:bold;
}

#NaslovGrupa
{
    min-height:50px;
    width:100%;
    font-size:1.2em;
    background-color:Gray;
    border:1px Solid Black;
    cursor:pointer;
}
.RadBtn
{
    cursor:pointer;
    min-height:50px;
}
.RadBtn:hover span
{
    text-decoration:underline;
}
@media screen and (max-width: 480px)
{
      #kopcePrikazi input
    {
       font-size:1em;
       width:50%;
    }
    
    #Content
    {
        font-size:1em;
    }
     #ArtikalPrikaz
    {
        font-size:1.2em;
        height:40px;
        overflow:hidden;
    }
}

@media screen and (max-width: 320px) 
{
    #kopcePrikazi input
    {
       font-size:1em;
       width:50%;
    }
    #Content
    {
        font-size:1em;
    }
    #ArtikalPrikaz
    {
        font-size:1em;
        height:40px;
        overflow:hidden;
    }
}

@media screen and (min-width: 1280px)
{
    #OrgEd select, #GrpOrgEd select
    {
        height:70px;
        font-size:1.5em;
    }
    #kopcePrikazi input
    {
       font-size:1.2em;
       width:50%;
       height:70px;
    }
}
</style>
</head>
<body>
    <form id="form1" runat="server">
<%--    <div id="Content">--%>
    <fieldset id="Content">
            <legend  onclick="ekran()">Kартица на артикал</legend>
    <div id="Grupa1" Grupa="1" Prikazi="DA"><%-- GRUPA1--%>

        <div id="Sodrzina">

                <div id="OrgEd">
                   Организациона единица
                   <br />
                    <asp:DropDownList ID="ddlOrgEd" runat="server">
                    </asp:DropDownList>
               
                </div>
                <div id="GrpOrgEd">
                Група на организациони единици
                <br />

                    <asp:DropDownList ID="ddlGrpOrgEd" runat="server">
                    </asp:DropDownList>
                </div>
                <div id="SifraArt">
                Артикал
                <br />
                    <span id ="ImeArtIzbor" runat="server" nasid="ImeArtIzbor"></span>
                        <asp:TextBox ID="tboxSifraArt" nasId="tboxTest" onkeyup="povikajAjax(this.value)" runat="server" autocomplete="off" SifraArt=""></asp:TextBox>
                <div id="SearchGrid">

                </div>
                </div>
            </div><%-- od Sodrzina--%>

        </div><%-- Od Grupa1--%>

        <div id="Grupa2" Grupa="2" Prikazi="NE"><%--  GRUPA 2--%>
        <div id="NaslovGrupa">Цени</div>
        <div id="Sodrzina">
                <div id="Ceni">
                    <div class="RadBtn">
                    <input id="Test1" runat="server" type="radio"  name="group1" value="DA" /><span> Маг.на.влез,прод.на излез</span> <br />
                    </div>
                    <div class="RadBtn">
                    <input id="Test2" runat="server" type="radio" name="group1" value="DA" /><span>Наб.на.влез,прод.на излез </span><br />
                    </div>
                    <div class="RadBtn">
                    <input id="Test3" runat="server" type="radio" name="group1" value="DA" /><span>По набавни цени </span><br />
                    </div>
                    <div class="RadBtn">
                    <input id="Test4" runat="server" type="radio" name="group1" value="DA" /><span>Плански цени </span><br />
                    </div>
                    <div class="RadBtn">
                    <input id="Test5" runat="server" type="radio" name="group1" value="DA" /><span>Образец МЕТГ </span><br />
                    </div>


                </div>
        </div>
        
        </div><%-- Od Grupa 2--%>

            <div id="kopcePrikazi">

                <asp:Button ID="btnPrikazi" runat="server" Text="Прикажи" OnClick="btnPrikazi_Click"  />

            </div>

       </fieldset>
<%--    </div>--%>

    </form>

</body>
</html>
