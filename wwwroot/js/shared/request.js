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

    static async post(url, data) {
        const response = await fetch(url, {
            method: "POST",
            headers: {
                Bearer: "Bearer " + localStorage.getItem("token"),
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });

        return response.json();
    }

    static async put(url, data) {
        const response = await fetch(url, {
            method: "PUT",
            headers: {
                Bearer: "Bearer " + localStorage.getItem("token"),
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });

        return response.json();
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
