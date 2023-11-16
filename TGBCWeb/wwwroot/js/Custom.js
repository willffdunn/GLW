<script>
    var numericValueElement = document.getElementById('numericValue');
    var previousButton = document.getElementById('previous');
    var nextButton = document.getElementById('next');

    previousButton.addEventListener('click', function () {
        var currentVal = parseInt(numericValueElement.innerText);
        if (currentVal > 1) {
        numericValueElement.innerText = currentVal - 1;
    updateModel(parseInt(numericValueElement.innerText));
        }
    });

    nextButton.addEventListener('click', function () {
        var currentVal = parseInt(numericValueElement.innerText);
    numericValueElement.innerText = currentVal + 1;
    updateModel(parseInt(numericValueElement.innerText));
    });

    function updateModel(newKeyValue) {
        $.ajax({
            type: "POST",
            url: '@Url.Action("GPScore", "GolfScoreController")',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(GPScoreVM),
            success: function (partialView) {
                $('#otherFields').html(partialView);
                wireUpPlusMinusEvents();
            },
            error: function () {
                alert('Error updating the model.');
            }
        });
    }

    function wireUpPlusMinusEvents() {
        // Plus and minus button event handling
        // ...

        $('#save').click(function () {
            updateModel(parseInt($('#numericValue').text()));
        });
    }

    wireUpPlusMinusEvents(); // Initial wiring
</script>
