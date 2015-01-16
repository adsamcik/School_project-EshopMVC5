var productsCount = 10;

var useJSON = true;

var productReviews = new Array();

var positiveReviews = ["Tento produkt funguje jak ma.", "S timto produktem jsem spokojen", "Mohu jen doporucit", "Splnuje me pozadavky.", "Dcera je s timto produktem spokojen.", "Vlastnim jiz mnoho let a funguje vyborne.", "Vlastnik obchodu mi vysel vstric, doporucuji!"];

var negativeReviews = ["Tento produkt nedoporucuji.", "S timto produktem nejsem spokojen", "Produkt byl velke zklamani.", "Nefunguje, uz jsem produkt 3x reklamoval.", "Popis neodpovida fungovani zbozi, nekupovat!", "Vlastnik obchodu je nevstricny, nekupovat!"];

var authors = ["Ing. Antoš Petr ", "Mgr. Bláhová Ludmila ", "Ing. Eliáš Antonín CSc.", "Mgr. Fiala Libor ", "Mgr. Filip Petr ", "Mgr. Hromádko Přemysl ", "Bc. Ivon Pavel ", "Mgr. Klein Ivan ", "Mgr. Kleinová Radka ", "PhDr. Kotek Vlastislav ", "MgA. Kout Pavel ", "Ing. Kratochvíl Aleš ", "Mgr. Mašek Petr ", "Ing. Matějková Hana ", "PaedDr. Megvinet Chucesovová Ivana ", "PhDr. Novák Vladimír ", "Mgr. Šafránková Hana ", "Ing. Šibrava Ondřej ", "Šimek Michael ", "Ing. Týfová Ilse ", "Mgr. Vaněčková Jitka ", "Ing. Vodička Martin ", "Mgr. Vostárek Tomáš ", "Mgr. Záveská Naděžda ", "Ing. Zima Václav "];

var lastID = 0;
if (useJSON) document.write("[");
for (var i = 1; i < productsCount + 1; i++) {
    var random = Math.round(Math.random() * 50);
    for (var z = 0; z < random; z++) {
        var positiveReview = positiveReviews[Math.floor(Math.random() * positiveReviews.length)];

        var negativeReview = negativeReviews[Math.floor(Math.random() * negativeReviews.length)];

        var author = authors[Math.floor(Math.random() * authors.length)];

        var date = new Date().toISOString().slice(0, 19).replace('T', ' ');


        if (useJSON) {
            productReviews[i] = JSON.stringify({
                "ReviewID": i + lastID+z,
                "productID": i,
                "Plus": positiveReview,
                "Minus": negativeReview,
                "Author": author.substring(0, author.length - 1),
                "ReviewDate": date
            });

                    if (i + 1 == productsCount+1 && z + 1 == random) document.write(productReviews[i]);
        else document.write(productReviews[i] + ",");
        } else {
            productReviews[i] = "insert into Review (ProductID, Plus, Minus, Author, ReviewDate) values(" + i + ", '" + positiveReview + "', '" + negativeReview + "', '" + author.substring(0, author.length - 1) + "', '" + date + "');";
        document.write(productReviews[i]);
        }
    }
    lastID += random;
}
if (useJSON) document.write("]");