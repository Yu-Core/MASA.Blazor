import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import commonjs from "@rollup/plugin-commonjs";
import json from "@rollup/plugin-json";
import resolve from "@rollup/plugin-node-resolve";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/wrappers/markdown-it/index.ts",
  output: [
    {
      file: "../Masa.Blazor.JSComponents.MarkdownIt/wwwroot/markdown-it.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), json(), resolve(), commonjs(), terser()],
});
