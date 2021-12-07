import styled from "styled-components";
import { Button, Card } from "antd";

export const FilmsContainer = styled(Card)`
  margin: 0 auto;
  max-width: 1200px;
  display: flex;
  flex-direction: column;
  align-items: center;
`;

export const StyledButton = styled(Button)`
  height: 30px;
  width: 150px;
  font-size: 16px;
`;
