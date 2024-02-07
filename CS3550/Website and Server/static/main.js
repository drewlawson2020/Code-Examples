import { $ } from "/static/jquery/src/jquery.js";
export function make_table_sortable($table)
{
    let cellIndex = 0;
    
    let setOfSortableHeaders = $table.find("th.sortable")
    setOfSortableHeaders.on("click", (e) => {
        let $cell = $(e.target);
        cellIndex = $cell[0].cellIndex

        if ($cell.hasClass("sort-asc"))
        {
            var row = $table.find("tbody tr")
            var sorted = $(row).toArray().sort(desc_sort)
            $cell.removeClass("sort-asc")
            $cell.addClass("sort-desc")
        }
        else if ($cell.hasClass("sort-desc"))
        {
            var row = $table.find("tbody tr")
            var sorted = $(row).toArray().sort(original_sort)
            $cell.removeClass("sort-desc")
            $cell.addClass("sort-original")
        }
        else if ($cell.hasClass("sort-original"))
        {
            var row = $table.find("tbody tr")
            var sorted = $(row).toArray().sort(asc_sort)
            $cell.removeClass("sort-original")
            $cell.addClass("sort-asc")
        }
        else
        {
            var row = $table.find("tbody tr")
            var sorted = $(row).toArray().sort(asc_sort)
            $cell.addClass("sort-asc")
        }
        $("tbody").append($(sorted))
    })


    function asc_sort(a, b){
        let x = parseFloat($(a).children().eq(cellIndex).attr("data-value"))
        let y = parseFloat($(b).children().eq(cellIndex).attr("data-value"))
        if (x < y)
        {
        return -1
        }
        else if (x > y)
        {
        return 1
        }
        return 0
    }

    function desc_sort(a, b){
        let x = parseFloat($(a).children().eq(cellIndex).attr("data-value"))
        let y = parseFloat($(b).children().eq(cellIndex).attr("data-value"))
        if (x > y)
        {
        return -1
        }
        else if (x < y)
        {
        return 1
        }
        return 0
    }

    function original_sort(a, b){
        let x = parseFloat($(a).data("index"))
        let y = parseFloat($(b).data("index"))
        if (x < y)
        {
        return -1
        }
        else if (x > y)
        {
        return 1
        }
        return 0
    }


}
export function make_grade_hypothesized($table)
{
    var button = $('<button>', {
        text: 'Hypothesized',
        id: 'gradeHypothesizedBtn',
    });
    
    

    $table.before(button);
    button.on("click", (e) => {
        if ($table.hasClass("hypothesized"))
        {
            $table.removeClass("hypothesized")
            button[0].textContent = "Hypothesized"
            let length = $table.find("tbody tr").length
            for (let i = 0; i < length; i++) 
            {
                var value = $table.find("tbody tr").eq(i).children().last()
                if (value.attr("data") == "Ungraded" || value.attr("data") == "Not Due")
                {
                    var textInput = $('<tr>', {
                        text: value.attr("data"),

                    });
                    textInput.attr('data-value', value.attr("data"))
                    textInput.attr('data-weight', value.attr("data-weight"))
                    value.replaceWith(textInput);
                }
            }
        }
        else
        {
            $table.addClass("hypothesized")
            button[0].textContent = "Actual Grades"
            let length = $table.find("tbody tr").length
            for (let i = 0; i < length - 1; i++) 
            {
                var value = $table.find("tbody tr").eq(i).children().last()
                if (value.attr("data-value") == "Ungraded" || value.attr("data-value") == "Not Due")
                {
                    var textInput = $('<input>', {
                        input: 'text',
                        id: i,
                    });
                    textInput.attr('data', value.attr("data-value"))
                    textInput.attr('data-weight', value.attr("data-weight"))
                    value.replaceWith(textInput);
                    
                }
            }
        }
    })
    $(document).on('click', function(e) {
        if ($table.hasClass("hypothesized")) {
            let totalWeight = 0;
            let totalPoints = 0;
                if (e.target.localName != 'input' && e.target.id != 'gradeHypothesizedBtn')
                {
                let length = $table.find("tbody tr").length
                for (let i = 0; i < length - 1; i++)
                    {
                        var value = $table.find("tbody tr").eq(i).children().last()
                        if (value.attr("data-value") == "Missing")
                        {
                            totalWeight = totalWeight + Number(value.attr("data-weight"))
                        }
                        else
                        {
                            if (value[0].localName == 'input')
                            {
                                if (isNaN(value[0].value))
                                {
                                    totalWeight = totalWeight + Number(value.attr("data-weight"))
                                }
                                else
                                {
                                    totalPoints = totalPoints + Number(value[0].value)
                                    totalWeight = totalWeight + Number(value.attr("data-weight"))
                                }
                            }
                            else
                            {
                                let score = value.attr("data-value")
                                score = score.substring(0, score.length - 1);

                                totalPoints = totalPoints + Number(score)
                                totalWeight = totalWeight + Number(value.attr("data-weight"))
                            }
                        }
                }
                let totalScore = totalPoints / totalWeight
                totalScore = (totalScore * 100).toFixed(3)

                length = $table.find("tbody tr").length

                var value = $table.find("tbody tr").children().last()
                var textInput = $('<th>', {
                    text: totalScore,
                });

                value.replaceWith(textInput);
                }
        }
    });

}

export function make_form_async(form)
{
    let url = form.attr("action");
    const formData = new FormData(form[0]);
    formData.get("csrfmiddlewaretoken")
form.on("submit", (e) =>{
        e.preventDefault();
        $.ajax(url, {
            url: url,
            mimeType: "encStype",
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            mimeType: "enctype",
            success: function(response){
                $(form).replaceWith("<p>Success!</p>")
            },
            error: function(response){
                console.log("Error: File is invalid")
            }
        })
        form.attr("disabled")
    })
}