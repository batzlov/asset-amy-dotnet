export class HttpRequest {
    static async get(url) {
        const response = await fetch(url, {
            method: "GET",
            headers: {
                Bearer: "Bearer " + localStorage.getItem("token"),
                "Content-Type": "application/json",
            },
        });

        return response.json();
    }

    static async post(url, data, { onSuccess, onError } = {}) {
        const response = fetch(url, {
            method: "POST",
            headers: {
                Bearer: "Bearer " + localStorage.getItem("token"),
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        })
            .then(async (response) => {
                if (response.status >= 400) {
                    const json = await response.json();
                    const errorMessage = json.message || response.statusText;
                    throw new Error(errorMessage);
                }

                return response.json();
            })
            .then((json) => {
                if (onSuccess) {
                    onSuccess(json);
                }
            })
            .catch((error) => {
                if (onError) {
                    onError(error);
                }
            });
    }

    static async put(url, data) {
        const response = fetch(url, {
            method: "PUT",
            headers: {
                Bearer: "Bearer " + localStorage.getItem("token"),
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        })
            .then(async (response) => {
                if (response.status >= 400) {
                    const json = await response.json();
                    const errorMessage = json.message || response.statusText;
                    throw new Error(errorMessage);
                }

                return response.json();
            })
            .then((json) => {
                if (onSuccess) {
                    onSuccess(json);
                }
            })
            .catch((error) => {
                if (onError) {
                    onError(error);
                }
            });
    }

    static async delete(url) {
        const response = await fetch(url, {
            method: "DELETE",
            headers: {
                Bearer: "Bearer " + localStorage.getItem("token"),
                "Content-Type": "application/json",
            },
        });

        return response.json();
    }
}
