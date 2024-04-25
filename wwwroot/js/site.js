// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(function(){
    orderBtn = $('#resetOrderBtn');
    shopBtn = $('#orderByShopBtn');
    categoryBtn = $('#orderByCategoryBtn'); 
    countryBtn = $('#orderByCountryBtn');
    dateBtn = $('#orderByDateBtn');
    receiptsList = $('#receiptsList');
    receiptsShop = $('#receiptsListByShop');
    receiptsDate = $('#receiptsListByDate');
    receiptsCategory = $('#receiptsListByCategory');
    receiptsCountry = $('#receiptsListByCountry');

    receiptsCategory.hide();
    receiptsCountry.hide();
    receiptsDate.hide();
    receiptsShop.hide();
    shopBtn.on('click', function(){
        receiptsList.hide();
        receiptsCategory.hide();
        receiptsCountry.hide();
        receiptsDate.hide();
        receiptsShop.show();
    })
    dateBtn.on('click', function() {
        receiptsList.hide();
        receiptsCategory.hide();
        receiptsCountry.hide();
        receiptsShop.hide();
        receiptsDate.show();
    })
    categoryBtn.on('click', function(){
        receiptsList.hide();
        receiptsCountry.hide();
        receiptsDate.hide();
        receiptsShop.hide();
        receiptsCategory.show();
    })
    countryBtn.on('click', function(){
        receiptsList.hide();
        receiptsCategory.hide();
        receiptsDate.hide();
        receiptsShop.hide();
        receiptsCountry.show();
    })
    orderBtn.on("click", function(){
        receiptsCategory.hide();
        receiptsCountry.hide();
        receiptsDate.hide();
        receiptsShop.hide();
        receiptsList.show();
    })
})

$(function () {
    $('#dropdowncontent1').hide();
    $('#dropdownbtn1').on('click', function () {
        $('#dropdowncontent1').slideToggle();
    });
    $('#dropdowncontent2').hide();
    $('#dropdownbtn2').on('click', function () {
        $('#dropdowncontent2').slideToggle();
    });
    $('#dropdowncontent3').hide();
    $('#dropdownbtn3').on('click', function () {
        $('#dropdowncontent3').slideToggle();
    });
    $('#dropdowncontent4').hide();
    $('#dropdownbtn4').on('click', function () {
        $('#dropdowncontent4').slideToggle();
    });
});

$(function () {
    $('#chart-area-months').hide();
    $('#graphMonthBtn').on('click', function () {
        $('#chart-area').hide();
        $('#chart-area-months').show();
        $('#graphYearBtn p').removeClass("active");
        $('#graphMonthBtn p').addClass("active");
    });

    $('#graphYearBtn').on('click', function () {
        $('#chart-area-months').hide();
        $('#chart-area').show();
        $('#graphMonthBtn p').removeClass("active");
        $('#graphYearBtn p').addClass("active");
    });
});


function addLineItem() {
    const lineItem = document.createElement('div');
    lineItem.classList.add('line-item');
    lineItem.innerHTML = `
        <label>Name:</label>
        <input type="text" name="itemName[]">
        <br>
        <label>Price (of 1):</label>
        <input type="number" name="itemPrice[]">
        <br>
        <label>Quantity:</label>
        <input type="number" name="itemQuantity[]">
    `;
    document.getElementById('lineItems').appendChild(lineItem);
}

function removeLineItem() {
    const lineItems = document.querySelectorAll('.line-item');
    if (lineItems.length > 1) {
        const lastItem = lineItems[lineItems.length - 1];
        lastItem.parentNode.removeChild(lastItem);
    }
}


