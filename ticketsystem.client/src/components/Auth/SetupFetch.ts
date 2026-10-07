const originalFetch = window.fetch;

window.fetch = async (...args) => {
    const response = await originalFetch(...args);

    if (
        response.status === 403 &&
        window.location.pathname !== "/login"
    ) {
        window.location.href = "/login";
    }

    return response;
};