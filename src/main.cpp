#include "CLI/App.hpp"

#define SUCCESS return 0

int main() {
    CLI::App app{APP_DESCRIPTION, APP_NAME};

    auto *init = app.add_subcommand("init", "Creates a new Luna project in the current directory.");
    init->callback();

    app.require_subcommand();

    CLI11_PARSE(app)
    SUCCESS;
}
