const ApiUrl = "https://localhost:7036/api/Stations";

export async function fetchStations( queryParams ) {
    const params = new URLSearchParams();

    Object.entries(queryParams).forEach(([key, value]) => {
        if (value !== '' && value !== null && value !== undefined) {
            params.append(key, value);
        }
    });

    const response = await fetch(`${ApiUrl}?${params}`);
    return await response.json();
}
