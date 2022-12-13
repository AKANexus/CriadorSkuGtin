$(document).ready(function () {
    myHeaders.append("Content-Type", "application/json");
    GetData();
    gravarNovoModal = new bootstrap.Modal(document.getElementById("gravarNovoModal"), {
        keyboard: false
    });
});
var gravarNovoModal;
var myHeaders = new Headers();
var descricaoField = document.getElementById("descricaoField");
var abreviaturaField = document.getElementById("abreviaturaField");
async function GetData(pageNum = 1) {



    const meuBody = {};

    //meuBody.columns = null;
    //filters.map(element => {
    //    return {
    //        columnName: element.column,
    //        filter: {
    //            initialValue: element.text,
    //            terminalValue: element.textSecondary,
    //            filterType: element.filterType
    //        }
    //    }
    //});
    //meuBody.ordering = null;
    //[{ columnName: orderPrefArray[0].columnName, direction: orderPrefArray[0].direction }];
    meuBody.pageNumber = pageNum;
    meuBody.recordsPerPage = 10;
    //ticketsPerPage;

    const requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: JSON.stringify(meuBody)
    };

    const post = await fetch(`/Dados/GetFornecedoresPaged/`, requestOptions);
    const response = post.text();

    if (post.status >= 400) {
        alert("Deu ruim");
    }
    else {
        const receivedData = await response;
        DrawTabela(receivedData);
    }
}

//function ClickToRemove(uuid, descricao) {
//    return ``;
//}

function pageSelectorKeyDown(e) {
    if (e.code === "Enter" || e.code === "NumpadEnter") {
        const selector = document.getElementById("pageSelectorField");
        GetData(Math.min(selector.value, maxPage));
    }
}

async function remove(uuid, descricao) {
    if (confirm(`Deseja remover o cadastro ${descricao}? Essa ação não poderá ser desfeita!`) === true) {
        await fetch(`/Dados/RemoverFornecedor?uuid=${uuid}`, {method: "GET"});
        document.location.reload();
    }
}

function DrawTabela(data) {
    //debugger;
    const tabela = document.getElementById("tableBod");
    tabela.innerHTML = null;
    const parsedData = JSON.parse(data);
    parsedData.returnedData.map(element => {
        //alert(element.discard);
        //alert(element.descricao);
        tabela.insertAdjacentHTML("beforeend", `<tr><td>${element.descricao}</td><td>${element.abreviatura}</td><td><button class="btn btn-danger" onclick="remove('${element.discard}', '${element.descricao}')">Remover</button></td></tr>`);

    });
    AtualizaPaginacao(parsedData.currentPage, parsedData.maxPages, parsedData.recordsTotal, parsedData.recordsPerPage);

}

function AtualizaPaginacao(pagAtual, maxPags, maxRecords, recordsPerPage) {
    const paginacaoArea = document.getElementById("paginationButtons");
    const recordNumbers = document.getElementById("recordNumbering");
    recordNumbers.innerHTML = null;
    paginacaoArea.innerHTML = null;
    maxPage = maxPags;
    //recordNumbers.innerText = `Exibindo ${Math.min(maxRecords, recordsPerPage)} resultados de ${maxRecords}`;

    if (pagAtual === 1) {
        paginacaoArea.insertAdjacentHTML("beforeend", '<li class="disabled"><span style="font-size:1.5em;text-decoration:none;cursor:default">&#9198;</span></li>');
        paginacaoArea.insertAdjacentHTML("beforeend", '<li class="disabled"><span style="font-size:1.5em;text-decoration:none;cursor:default">&#9194;</span></li>');
    } else {
        paginacaoArea.insertAdjacentHTML("beforeend", `<li><a href=# onClick=GetData() style="font-size:1.5em;text-decoration:none">&#9198;</a></li>`);
        paginacaoArea.insertAdjacentHTML("beforeend", `<li><a href=# onClick=GetData(${pagAtual - 1}) style="font-size:1.5em;text-decoration:none">&#9194;</a></li>`);
    }
    paginacaoArea.insertAdjacentHTML("beforeend", `<input type="number" style="width:60px" value="${pagAtual}" id="pageSelectorField" />`);
    if (pagAtual === maxPags) {
        paginacaoArea.insertAdjacentHTML("beforeend", '<li class="disabled"><span style="font-size:1.5em;text-decoration:none;cursor:default">&#9193;</span></li>');
        paginacaoArea.insertAdjacentHTML("beforeend", '<li class="disabled"><span style="font-size:1.5em;text-decoration:none;cursor:default">&#9197;</span></li>');
    } else {
        paginacaoArea.insertAdjacentHTML("beforeend", `<li><a href=# onClick=GetData(${pagAtual + 1}) style="font-size:1.5em;text-decoration:none">&#9193;</a></li>`);
        paginacaoArea.insertAdjacentHTML("beforeend", `<li><a href=# onClick=GetData(${maxPags}) style="font-size:1.5em;text-decoration:none">&#9197;</a></li>`);
    }
    //const selector = document.getElementById("pageSelectorField");
    document.addEventListener("keydown", pageSelectorKeyDown);
}

function getById(id) {
    return document.getElementById(id);
}

function onGravarNovoButtonClick() {
    gravarNovoModal.show();
}

async function gravarFabricante() {

    const gravarNovoBody = {};
    gravarNovoBody.descricao = descricaoField.value;
    gravarNovoBody.abreviatura = abreviaturaField.value;
    gravarNovoBody.discard = `00000000-0000-0000-0000-000000000000`;

    const requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: JSON.stringify(gravarNovoBody)
    };

    const post = await fetch("/Dados/PostFabricante/", requestOptions);
    //const response = post.text();

    if (post.status === 409) {
        alert("Abreviatura já utilizada.");
    }
    else if (post.status >= 400) {
        alert("Deu ruim");
    }
    else {
        descricaoField.value = "";
        abreviaturaField.value = "";
        GetData();
        gravarNovoModal.hide();
    }

}