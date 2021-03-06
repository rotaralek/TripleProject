﻿(function ($) {
    "use strict";

    /*
     *  File Upload
     */
    $(document).on('change', '.ajax-file-upload #file', function () {
        var form = $(this).closest('form');
        var loading = form.find('.spinner-border')
        var file = form.find('input[name=file]')
        var files = file[0].files;
        var imagesList = [];
        var multiple = false;

        var formData = new FormData();

        for (var i = 0; i < files.length; i++) {
            formData.append('files', files[i]);
        }

        if (file.attr('multiple')) {
            multiple = true;
        }

        $.ajax({
            url: '/AjaxUpload',
            type: 'POST',
            data: formData,
            dataType: "json",
            processData: false,
            contentType: false,
            cache: false,
            processData: false,
            headers: {
                "Accept": "application/json",
                "RequestVerificationToken": form.find("input[name='__RequestVerificationToken']").val()
            },
            beforeSend: function () {
                loading.removeClass('d-none');
                form.find('.response').html('');
            },
            success: function (response) {
                for (var i = 0; i < response.length; i++) {
                    if (i != 0) {
                        imagesList += ';';
                    }

                    imagesList += response[i]['id'];

                    if (multiple) {
                        form.find('.response').append('<div class="square-image thumbnail img-thumbnail mr-2 mb-2" style="background-image: url(' + response[i]['path'] + ')"></div>');
                    } else {
                        form.find('.response').html('<div class="square-image thumbnail img-thumbnail mr-2 mb-2" style="background-image: url(' + response[i]['path'] + ')"></div>');
                    }
                }

                if (multiple) {
                    $(document).find('#GalleryId').attr('value', imagesList);
                } else {
                    $(document).find('#ImageId').attr('value', imagesList);
                }

                loading.addClass('d-none');
            }
        });
    });

    /*
     * TinyMce
     */
    tinymce.init({
        selector: 'textarea.tinymce',
        height: 500,
        menubar: false,
        plugins: [
            'advlist autolink lists link image charmap print preview anchor textcolor',
            'searchreplace visualblocks code fullscreen',
            'insertdatetime media table paste code help wordcount'
        ],
        toolbar: 'undo redo | formatselect | bold italic backcolor | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | removeformat | help'
    });

    /*
     * Carousel nav
     */
    var owlThmbnailSlider = function () {
        var slider = $(document).find('.owl-thumbnails-slider');
        var inner = $(document).find('.owl-thumbnails-inner');

        var owlThambnailSliderHeight = slider.height();
        var owlThambnailInnerHeight = inner.height();
        var owlThambnailItems = inner.find('a');
        var owlThambnailItemsLength = owlThambnailItems.length;
        var owlThambnailOneItemHeight = (owlThambnailInnerHeight / owlThambnailItemsLength) + 5;
        var counter = 1;

        inner.css({
            top: 0
        }).attr('data-offset', 0).attr('data-location', 1);

        owlThambnailItems.each(function () {
            var hash = $(this).attr('data-hash');

            $(this).removeAttr('data-hash');
            $(this).attr('href', '#' + hash);
            $(this).attr('data-counter', counter);

            counter++;
        });

        inner.on('click', 'a', function () {
            var inner = $(document).find('.owl-thumbnails-inner');
            var offset = parseInt(inner.attr('data-offset'));
            var prevLocation = parseInt(inner.attr('data-location'));
            var location = parseInt($(this).attr('data-counter'));

            if (location == prevLocation || location == owlThambnailItemsLength) {
                offset = offset;
            } else if (location == 1 || location == 2) {
                offset = 0;
            } else if (location < prevLocation) {
                offset = offset + owlThambnailOneItemHeight;
            } else if (location > prevLocation) {
                offset = offset - owlThambnailOneItemHeight;
            }

            inner.css({
                top: offset
            });

            inner.attr('data-offset', offset);
            inner.attr('data-location', location);
        });
    }

    if ($(document).find('.owl-thumbnails-slider').length) {
        owlThmbnailSlider();
    }

    /*
     * Crousel miltiple
     */
    $(document).find('.owl-carousel').each(function () {
        var slidesPerLine = parseInt($(this).attr('data-slides-per-line'));

        if ($(this).hasClass('owl-carousel-mono')) {
            $(this).owlCarousel({
                loop: false,
                margin: 30,
                nav: false,
                items: 1,
                URLhashListener: true,
                autoplayHoverPause: true,
                startPosition: 'URLHash'
            });

            $(this).find('a').attr('data-toggle', 'modal').attr('data-target', '#modalImage');
        } else {
            $(this).owlCarousel({
                loop: true,
                margin: 30,
                nav: false,
                responsive: {
                    0: {
                        items: 1
                    },
                    600: {
                        items: slidesPerLine - 1
                    },
                    1000: {
                        items: slidesPerLine
                    }
                }
            });
        }
    });

    /*
    * Modal image
    */
    $('#modalImage').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var image = button.attr('href');
        var modal = $(this);
        console.log(image);
        modal.find('.modal-body .image').attr('src', image);
    });

    /*
     * Views trigger
     */
    $(window).on('load', function () {
        if ($(document).find('.views-trigger').length) {
            var viewsContainer = $(document).find('.views-trigger');
            var type = viewsContainer.attr('data-type');
            var id = viewsContainer.attr('data-id');

            $.ajax({
                url: '/AjaxViewsIncrement',
                type: 'POST',
                data: {
                    type: type,
                    id: id
                },
                dataType: "json",
                cache: false,
                headers: {
                    "Accept": "application/json",
                    "RequestVerificationToken": viewsContainer.find("input[name='__RequestVerificationToken']").val()
                },
                beforeSend: function () {
                },
                success: function (response) {
                    //console.log(response);
                }
            });
        }
    });

    /*
     * Header fix on scroll
     */
    if ($(document).find('.tell-main-header').length) {
        var defaultHeaderValue = 0;

        $(document).find('.tell-main-header').css({
            transition: "all 300ms ease"
        });

        $(document).on('scroll', function (e) {
            var mainHeader = $(document).find('.tell-main-header');
            var documentOffset = $(this).scrollTop();
            var redLineOffset = mainHeader.position().top;
            var redLineHeight = mainHeader.innerHeight();

            if (defaultHeaderValue < redLineOffset) {
                defaultHeaderValue = redLineOffset;
            }

            if (documentOffset >= defaultHeaderValue) {
                if (!mainHeader.hasClass('fixed-top')) {
                    mainHeader.addClass('fixed-top');
                    mainHeader.after('<div class="header-size" style="height:' + redLineHeight + 'px"></div>');
                }
            } else {
                mainHeader.removeClass('fixed-top');
                $(document).find('.header-size').remove();
            }
        });
    }

    /*
     * Cookie policy toggle
     */
    $(document).on('click', '#cookieConsent button[data-cookie-string]', function (e) {
        document.cookie = e.target.dataset.cookieString;
        $(document).find('#cookieConsent').addClass('invisible');
    });

    if ($(document).find('#cookieConsent').length) {
        setTimeout(function () {
            $(document).find('#cookieConsent button[data-cookie-string]').trigger('click');
        }, 5000);
    }

    /*
     * Slug creator
     */
    $(document).on('keyup', '.slug-trigger', function () {
        var slugField = $(document).find('.slug-field');

        if (slugField.attr("readonly")) {
            return;
        }

        var fieldVal = $(this).val();
        fieldVal = fieldVal.toLowerCase();
        var fieldValLength = fieldVal.length;
        var translitVal = "";
        var slug = "";

        //Change cyrillic to translit
        var cyrillicToTranslit = {
            "а": "a",
            "б": "b",
            "в": "v",
            "ґ": "g",
            "г": "g",
            "д": "d",
            "е": "e",
            "ё": "e",
            "є": "ye",
            "ж": "zh",
            "з": "z",
            "и": "i",
            "і": "i",
            "ї": "yi",
            "й": "i",
            "к": "k",
            "л": "l",
            "м": "m",
            "н": "n",
            "о": "o",
            "п": "p",
            "р": "r",
            "с": "s",
            "т": "t",
            "у": "u",
            "ф": "f",
            "х": "h",
            "ц": "c",
            "ч": "ch",
            "ш": "sh",
            "щ": "sh'",
            "ъ": "",
            "ы": "i",
            "ь": "",
            "э": "e",
            "ю": "yu",
            "я": "ya",
            " ": "-"
        };
        for (var i = 0; i < fieldValLength; i++) {
            var letter = "";

            for (var c in cyrillicToTranslit) {
                if (fieldVal[i] == c) {
                    letter = cyrillicToTranslit[c]
                }
            }

            if (letter == "") {
                letter = fieldVal[i];
            }

            translitVal += letter;
        }

        slug = translitVal;

        slugField.val(slug);
    });

    //auto-parent-select for checkboxes
    $(document).on('click', '.auto-parent-select input[type=checkbox]', function () {
        checkParent(this);
        checkChild(this);
        uncheckChild(this);
    });

    var checkParent = function (item) {
        if ($(item).is(":checked")) {
            var newItem = $(item).closest('.sub-item').closest('.nav-item').find('> .form-check > label > input[type=checkbox]')
            newItem.each(function () {
                $(this).prop('checked', true);
            });

            checkParent(newItem);
        }
    };

    var checkChild = function (item) {
        if ($(item).is(":checked")) {
            var newItem = $(item).closest('.nav-item').find('input[type=checkbox]')
            newItem.each(function () {
                $(this).prop('checked', true);
            });

            checkParent(newItem);
        }
    };

    var uncheckChild = function (item) {
        if (!$(item).is(":checked")) {
            $(item).closest('.nav-item').find('input[type=checkbox]').prop('checked', false);
        }
    };

    /*
     * Price slider
     */
    var priceSlider = function () {
        if (!$(document).find("#slider-range").length) {
            return;
        }

        var range = $(document).find("#slider-range");
        var minPrice = $(document).find("#minPrice");
        var maxPrice = $(document).find("#maxPrice");
        var price = $(document).find("#price");
        var min = parseInt(price.attr('min'));
        var max = parseInt(price.attr('max'));

        minPrice.val(minPrice.val());
        maxPrice.val(maxPrice.val());

        range.slider({
            range: true,
            min: min,
            max: max,
            values: [minPrice.val(), maxPrice.val()],
            slide: function (event, ui) {
                price.val("$" + ui.values[0] + " - $" + ui.values[1]);
                minPrice.val(ui.values[0]);
                maxPrice.val(ui.values[1]);
            }
        });

        price.val("$" + range.slider("values", 0) + " - $" + range.slider("values", 1));
    };
    priceSlider();

    /*
     * Catalog products load
     */
    var catalogProductsLoad = function (selector) {
        var currency = ['MDL', 'USD', 'EUR'];
        var viewsContainer = $(document).find('.views-trigger');

        $(selector).each(function () {
            var item = $(this);
            var shopId = item.attr('data-shop-id');
            var catalogId = item.attr('data-catalog-id');

            $.ajax({
                url: '/AjaxCatalogProductsLoad',
                type: 'POST',
                data: {
                    shopId: shopId,
                    catalogId: catalogId
                },
                dataType: "json",
                cache: false,
                headers: {
                    "Accept": "application/json",
                    "RequestVerificationToken": viewsContainer.find("input[name='__RequestVerificationToken']").val()
                },
                beforeSend: function () {
                },
                success: function (response) {
                    var html = "";

                    console.log(response)

                    for (var i = 0; i < response.length; i++) {
                        html += '<div class="col-md-4 mb-3"><article class="card">';

                        if (response[i]['image'] != null) {
                            html += '<a href="/products/' + response[i]['id'] + '">' +
                                '<div class="square-image card-img-top" style="background-image: url(' + response[i]['image']['path'] + ')"></div>' +
                                '</a>';
                        }

                        var price = response[i]['price'];
                        price = price.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1 ');

                        html += '<div class="card-body">' +
                            '<h3 class="h5 trancate"><a href="/products/' + response[i]['id'] + '">' + response[i]['title'] + '</a></h3>' +
                            '<p class="trancate">' + response[i]['description'] + '</p>' +
                            '<p class="text-red h5">' + price + ' ' + currency[response[i]['currency']] + '</p>' +
                            '</div>' +
                            '</article></div>';
                    }

                    item.html(html);
                }
            });
        });
    };

    catalogProductsLoad('.ajax-catalog-load');

    /*
     * Product form submit
     */
    var productFormSubmit = function () {
        $(document).on('submit', '.product-form', function (e) {
            e.preventDefault();

            var form = $(this);
            var formData = {}
            var formFields = form.serialize();
            formFields = formFields.split('&');

            for (var i = 0; i < formFields.length; i++) {
                var localField = formFields[i].split('=');
                formData[localField[0]] = localField[1];
            }
            console.log(formData);

            $.ajax({
                url: '/AjaxAddToCart',
                type: 'POST',
                data:  formData,
                dataType: "json",
                cache: false,
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "RequestVerificationToken": form.find("input[name='__RequestVerificationToken']").val()
                },
                beforeSend: function () {
                },
                success: function (response) {
                    console.log(response);
                }
            });
        });
    };

    /*
     * Add to cart
     */
    $(document).on('click', '.add-to-cart', function () {

        productFormSubmit();
    });

    /*
     * Buy now
     */
    $(document).on('click', '.buy-now', function () {


    });
})(jQuery);