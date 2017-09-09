function AdminHelper(){
};

AdminHelper.addinvoiceline = function (action) {
    if (!!action) {
        switch (action) {
            case "AddLine":
                var count = $("#Linetable tbody").find("tr").length - 1
                $.post("../Admin/AddInvoiceLine", { number: count }, function (result) {
                    $("#Linetable tbody").find("tr:last").before(result); });
                break;
            case "DeleteLine":
                if ($("#Linetable tbody").find("tr").length < 3){  return false; }
                $("#Linetable tbody").find("tr:last").prev().remove();
                AdminHelper.CalculateInvoice();
                break;
       }
    }
};

AdminHelper.CalculateInvoice = function () {
    var totalbag = 0, TotalBagKg = 0; TotalAmountBeforeTax = 0, TotalIGSTAmount = 0; TotalSGSTAmount = 0; TotalCGSTAmount = 0; TotalGSTAmount = 0; TotalAmountAfterTax = 0; TotalAfterRoundoff = 0;
    $('#Linetable > tbody > tr').not(':last').each(function (index, value) {
        var currentBag = $(this).find("input[name*='Nos_Bag']").val();
        if (!!currentBag && $.isNumeric(currentBag)) {

            var Uom = $(this).find("input[name*='Uom']").val();
            if (!!Uom && $.isNumeric(Uom)) {
                totalbag += parseFloat(currentBag);
                TotalBagKg = TotalBagKg + (parseFloat(currentBag) * parseFloat(Uom));
            }
            var rate = $(this).find("input[name*='Rate']").val();
            if (!!rate && $.isNumeric(rate)) {
                var CGSTAmount = 0, IGSTAmount = 0, SGSTAmount = 0;
                var total = parseFloat(currentBag) * parseFloat(rate);
                $(this).find("input[name*='Amount']").val(total);
                var Discount = $(this).find("input[name*='Discount']").val();
                if (!!Discount) {
                    total = total - (parseFloat(Discount) * parseFloat(currentBag));
                }

                $(this).find("input[name*='TaxableValue']").val(total);
                TotalAmountBeforeTax += total;
                var CGSTRate = $(this).find("input[name*='CGST_Rate']").val();
                if (!!CGSTRate) {
                    CGSTAmount = parseFloat(CGSTRate) * total / 100;
                    $(this).find("input[name*='CGST_Amount']").val(CGSTAmount);
                    TotalCGSTAmount += CGSTAmount;
                }
                else {
                    $(this).find("input[name*='CGST_Amount']").val('')
                }

                var IGSTRate = $(this).find("input[name*='IGST_Rate']").val();
                if (!!IGSTRate) {
                    IGSTAmount = parseFloat(IGSTRate) * total / 100;
                    $(this).find("input[name*='IGST_Amount']").val(IGSTAmount);
                    TotalIGSTAmount += IGSTAmount;
                }
                else {
                    $(this).find("input[name*='IGST_Amount']").val('');
                }

                var SGSTRate = $(this).find("input[name*='SGST_Rate']").val();
                if (!!SGSTRate) {
                    SGSTAmount = parseFloat(SGSTRate) * total / 100;
                    $(this).find("input[name*='SGST_Amount']").val(SGSTAmount);
                    TotalSGSTAmount += SGSTAmount;
                }
                else {
                    $(this).find("input[name*='SGST_Amount']").val('');
                }
                total += (CGSTAmount + IGSTAmount + SGSTAmount);
                $(this).find("td[id*='TotalLabel']").html(total);
                $(this).find("input[name*='Total']").val(total);
                TotalAmountAfterTax += total;
            }

        }

    });
    var lastrow = $('#Linetable > tbody > tr:last');
    lastrow.find("#Total-Nos-Bag").html(totalbag.toFixed(2));
    lastrow.find("#Total-Taxable-Value").html(TotalAmountBeforeTax.toFixed(2));
    lastrow.find("#Total-CGST-Amount").html(TotalCGSTAmount.toFixed(2));
    lastrow.find("#Total-SGST-Amount").html(TotalSGSTAmount.toFixed(2));
    lastrow.find("#Total-IGST-Amount").html(TotalIGSTAmount.toFixed(2));
    lastrow.find("#Total-Amount").html(TotalAmountAfterTax.toFixed(2));
    $("#TotalAmountBeforeTaxLabel").html(TotalAmountBeforeTax.toFixed(2));
    $("#Total_CGSTLabel").html(TotalCGSTAmount.toFixed(2));
    $("#Total_SGSTLabel").html(TotalSGSTAmount.toFixed(2));
    $("#Total_IGSTLabel").html(TotalIGSTAmount.toFixed(2));
    $("#TotalAmountAfterTaxLabel").html(TotalAmountAfterTax.toFixed(2));
    TotalGSTAmount = TotalIGSTAmount + TotalSGSTAmount + TotalCGSTAmount;
    $("#Total_GSTLabel").html(TotalGSTAmount.toFixed(2));
    $("#TotalAfterRoundOffLabel").html(Math.round(TotalAmountAfterTax));
    $("#Total_Nos_KgLabel").html(TotalBagKg.toFixed(2));
    $("#TotalAmountBeforeTax").val(TotalAmountBeforeTax.toFixed(2));
    $("#Total_Nos_Kg").val(TotalBagKg.toFixed(2));
    $("#Total_Nos_Bag").val(totalbag.toFixed(2));
    $("#Total_CGST").val(TotalCGSTAmount.toFixed(2));
    $("#Total_IGST").val(TotalIGSTAmount.toFixed(2));
    $("#Total_SGST").val(TotalSGSTAmount.toFixed(2));
    $("#Total_GST").val(TotalGSTAmount.toFixed(2));
    $("#TotalAmountAfterTax").val(TotalAmountAfterTax.toFixed(2));
    $("#TotalAfterRoundOff").val(Math.round(TotalAmountAfterTax));
    $("#TotalAmountBeforeTax").val(TotalAmountBeforeTax.toFixed(2));
    
};

AdminHelper.DeleteInvoice = function (Invoiceid) {
    if (!!Invoiceid) {
        $.post("../Admin/DeleteInvoice", { Invoiceid: Invoiceid }, function (result) {
            if (!!result) {
                $("#dataTable").find("tbody").html($(result).find("tbody").html());
            }
            else {
                CommonJS.showMessage(CommonJS.State.ERROR, "Something goes wrong please try after some time");
            }
        });
    }
};

AdminHelper.GetInvoiceReport = function(daterange) {
    debugger;
    if (!!daterange) {
        $.post("../Admin/GetReport", { dateRange: daterange }, function (result) {
            if (!!result) {
                $("#InvoiceGSTReportContainer").html($(result).find("#InvoiceGSTReportContainer").html());
               $("#print").removeClass("disabled");
             }
            else {
                $("#InvoiceGSTReportContainer").html("<div> no record found </div>");
                $("#print").addClass("disabled");
            }
        });

    }
};



