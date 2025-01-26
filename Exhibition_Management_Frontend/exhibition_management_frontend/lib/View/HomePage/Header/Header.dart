import 'package:flutter/material.dart';
import 'package:exhibition_management_frontend/Tools/ColorPalette.dart';

class Header extends StatelessWidget {
  final TextEditingController searchController;

  const Header({super.key, required this.searchController});

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: EdgeInsets.only(
        top: MediaQuery.of(context).padding.top, // Add padding for status bar
        left: 8,
        right: 8,
        bottom: 8,
      ),
      decoration: const BoxDecoration(
        color: ColorPalette.lightBackgroundColor, // Apply background color
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            "Welcome to Star Exhibition",
            style: TextStyle(
              fontSize: 24,
              fontWeight: FontWeight.bold,
              color: ColorPalette.lightTextColor, // Text color
            ),
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Expanded(
                  child: TextField(
                controller: searchController,
                style: const TextStyle(
                  // Customize input text color
                  color: Colors.black, // Set the text color
                  fontSize: 16, // Optional: Set the font size
                ),
                decoration: InputDecoration(
                  hintText: "Search by Venue and Month...",
                  hintStyle: const TextStyle(
                    // Customize hint text style
                    color: Colors.grey, // Hint text color
                    fontSize: 16,
                  ),
                  filled: true,
                  fillColor: Colors.white, // Background color
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(8.0),
                    borderSide: BorderSide.none, // No border
                  ),
                  prefixIcon: const Icon(Icons.search,
                      color: Colors.black), // Icon color
                  contentPadding: const EdgeInsets.symmetric(vertical: 10.0),
                ),
              )),
              IconButton(
                icon:
                    const Icon(Icons.menu, color: ColorPalette.lightIconColor),
                onPressed: () {
                  showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return AlertDialog(
                        title: const Text("Settings"),
                        content: const Text("Settings options go here."),
                        actions: <Widget>[
                          TextButton(
                            onPressed: () {
                              Navigator.of(context).pop();
                            },
                            child: const Text("Close"),
                          ),
                        ],
                      );
                    },
                  );
                },
              ),
            ],
          ),
        ],
      ),
    );
  }
}
