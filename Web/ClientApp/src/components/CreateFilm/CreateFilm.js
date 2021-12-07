import React, { useState } from "react";
import "antd/dist/antd.css";
import { CreateFilmsContainer } from "./CreateFilm.styled";
import { Button, Input, DatePicker, InputNumber } from "antd";
import { postNewFilm } from "../../api/Api";
import moment from "moment";

const { TextArea } = Input;

export const CreateFilm = () => {
  const [filmId, setFilmId] = useState();
  const [filmTitle, setFilmTitle] = useState("");
  const [filmDescription, setFilmDescription] = useState("");
  const [filmRating, setFilmRating] = useState("");
  const [filmDate, setFilmDate] = useState(null);
  const [buttonText, setButtonText] = useState("Create Film!");

  function sendPostRequest() {
    debugger;
    const film = {
      id: filmId,
      title: filmTitle,
      description: filmDescription,
      rating: filmRating,
      releaseDate: filmDate,
      reviews: null,
      actors: null,
    };
    if (film.id && film.title) {
      postNewFilm(film);
      setButtonText("DONE🙂");
    }
  }

  return (
    <CreateFilmsContainer title="Create film">
      <Input.Group compact style={{ marginBottom: "16px" }}>
        <Input
          size="large"
          placeholder="Title"
          style={{ width: "80%" }}
          onChange={(e) => setFilmTitle(e.target.value)}
        />
        <InputNumber
          min="1"
          size="large"
          placeholder="ID"
          style={{ width: "20%" }}
          onChange={(e) => setFilmId(e)}
        />
      </Input.Group>
      <Input.Group compact style={{ marginBottom: "16px" }}>
        <DatePicker
          placeholder="Release date"
          style={{ width: "80%" }}
          onChange={(e) => setFilmDate(moment(e._d).format("YYYY-MM-DD"))}
        />
        <InputNumber
          style={{ width: "20%" }}
          placeholder="Rating"
          min="0"
          max="10"
          step="0.01"
          value={filmRating}
          onChange={(e) => setFilmRating(e)}
        />
      </Input.Group>
      <TextArea
        placeholder="Description"
        maxLength={1000}
        autoSize={{ minRows: 3 }}
        style={{ height: 120 }}
        value={filmDescription}
        onChange={(e) => setFilmDescription(e.target.value)}
      />
      <Button
        style={{ marginTop: "160px", width: "100%" }}
        type="primary"
        size={"large"}
        onClick={sendPostRequest}
      >
        {buttonText}
      </Button>
    </CreateFilmsContainer>
  );
};
