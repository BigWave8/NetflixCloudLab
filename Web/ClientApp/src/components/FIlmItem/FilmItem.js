import React from "react";
import "antd/dist/antd.css";
import { FilmItemContent } from "./FilmItem.styled";
import { Popover, Button } from "antd";
import moment from "moment";

export const FilmItem = (props) => {
  const { film } = props;

  const actors = (
    <div>
      {film.actors.map((actor) => (
        <p>
          {actor.name}
          {actor.surname}
          {actor.rating}✪
        </p>
      ))}
    </div>
  );

  const review = (
    <div>
      {film.reviews.map((review) => (
        <p>
          {review.user.username}: {review.text}
        </p>
      ))}
    </div>
  );

  return (
    <>
      <FilmItemContent>
        <h3>{film.title}</h3>
        <div>{moment(film.releaseDate).format()}</div>
        <div>Rating: {film.rating} ✪</div>
        <div>{film.description}</div>
        <Popover
          placement="bottomLeft"
          content={actors}
          title="Actors"
          trigger="hover"
        >
          <Button>Actors</Button>
        </Popover>
        <Popover
          placement="bottomLeft"
          content={review}
          title="Users review"
          trigger="hover"
        >
          <Button>Review</Button>
        </Popover>
      </FilmItemContent>
    </>
  );
};
