import axios from "axios";

const BaseRequest = axios.create({
  baseURL: "https://netflixcloudlabfunction.azurewebsites.net/api/film",
  responseType: "json",
});

export const getAllFilms = async () => {
  try {
    const films = await (await BaseRequest.get()).data;
    return films;
  } catch (e) {
    console.log(`😱 Axios request failed: ${e}`);
  }
};

export const postNewFilm = (film) => {
  try {
    debugger;
    BaseRequest.post("", film).then((response) => console.log(response));
  } catch (e) {
    console.log(`😱 Axios request failed: ${e}`);
  }
};
