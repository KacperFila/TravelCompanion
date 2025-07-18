const fs = require("fs");
const path = require("path");

try {
  const apiUrl = process.env.API_URL || "";

  const targetPath = path.resolve(
    __dirname,
    "../src/environments/environment.prod.ts"
  );

  const fileContent = `
        export const environment = {
        production: true,
        apiBaseUrl: '${apiUrl}'
        };
        `;

  fs.writeFileSync(targetPath, fileContent);
  console.log("✅ Successfully set up environment settings");
} catch (error) {
  console.error("❌ Failed to set up environment settings");
  console.error(error);
  process.exit(1);
}
