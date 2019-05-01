(function ($) {
    "use strict";

    $(document).on('change', '.ajax-file-upload #file', function () {
        var form = $(this).closest('form');
        var loading = form.find('.spinner-border')
        var file = form.find('input[name=file]')
        var files = file[0].files;
        var imagesList = '';
        var multiple = false;

        var formData = new FormData();

        for (var i = 0; i < files.length; i++) {
            formData.append('files', files[i]);
        }

        if (file.attr('multiple')) {
            multiple = true;
        }

        $.ajax({
            url: '/test',
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
            },
            success: function (response) {
                for (var i = 0; i < response.length; i++) {
                    if (i != 0) {
                        imagesList += ';';
                    }

                    imagesList += response[i]['id'];
                    form.find('.response').append('<img src="' + response[i]['path'] + '" class="img-thumbnail">');
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
})(jQuery);