import axios from "axios";

export const getApi = (url: string) =>
  axios.create({
    baseURL: url,
  });
