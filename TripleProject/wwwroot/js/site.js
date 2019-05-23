(function ($) {
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
     * Crousel miltiple
     */
    $(document).find('.owl-carousel').each(function () {
        var slidesPerLine = parseInt($(this).attr('data-slides-per-line'));

        if ($(this).hasClass('owl-carousel-mono')) {
            $(this).owlCarousel({
                loop: true,
                margin: 30,
                nav: false,
                items: 1
            });
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
    })

    /*
     * Views trigger
     */
    $(window).on('load', function () {
        if ($(document).find('.views-trigger').length) {
            var viewsContainer = $(document).find('.views-trigger');
            var type = viewsContainer.attr('data-type');
            var id = viewsContainer.attr('data-id');

            console.log(type);
            console.log(id);

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
                    console.log(response);
                }
            });
        }
    });
})(jQuery);