import React from "react";
import { render } from "react-dom";
import Nav from "./Nav";
import ProductTable from "./ProductTable";
import SqlInfo from "./SqlInfo";

const App = () => (
  <div>
    <Nav />
    <ProductTable />
    <SqlInfo />
  </div>
);

render(<App />, document.getElementById("root"));
