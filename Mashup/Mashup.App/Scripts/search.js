function search() {
    var searchQuery = encodeURIComponent($('#searchQuery').val());

    var url = $('#searchButton').data('searchurl');

    $.post(url + "?query=" + searchQuery, function (result) {
        var searchResult = $('#searchResult');
        searchResult.hide('fast');
        searchResult.html(result);
        searchResult.show('slow');
    });

}

function searchKeyPress(e) {
    // look for window.event in case event isn't passed in
    if (typeof e == 'undefined' && window.event) { e = window.event; }
    if (e.keyCode == 13) {
        search();
    }
}