import React, { Component, useState, useEffect } from "react";
import { FilmItem } from "./FIlmItem/FilmItem";
import { FilmsContainer } from "./Home.styled";
import { Button } from "antd";
import { useHistory } from "react-router-dom";
import { getAllFilms } from "../api/Api";

export class Home extends Component {
  constructor(props) {
    super(props);
    this.loadFilms = this.loadFilms.bind(this);
    this.state = {
      films: [],
    };
  }

  loadFilms() {
    getAllFilms().then((res) => this.setState({ films: res }));
  }

  componentDidMount() {
    const { films } = this.state;
    debugger;
    this.loadFilms();
  }

  render() {
    const { films } = this.state;
    return (
      <FilmsContainer title="Films">
        {films && films.map((film) => <FilmItem film={film}></FilmItem>)}
      </FilmsContainer>
    );
  }
}
