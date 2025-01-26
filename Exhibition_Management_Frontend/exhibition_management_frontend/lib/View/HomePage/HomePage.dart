import 'package:flutter/material.dart';
import 'package:exhibition_management_frontend/Tools/ColorPalette.dart'; // Import the ColorPalette
import 'package:exhibition_management_frontend/Models/ExhibitionCardDTO.dart';
import 'package:exhibition_management_frontend/View/HomePage/ExhibitionCard.dart'; // Adjust the path as needed
import 'package:exhibition_management_frontend/View/HomePage/Header/Header.dart'; // Import your Header widget
import 'package:exhibition_management_frontend/View/ExhibitionDetails/ExhibitionDetailsPage.dart'; // Import the DetailsPage
import 'package:exhibition_management_frontend/View/HomePage/FilterButton/FilterBtn.dart'; // Import the DetailsPage

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    final TextEditingController searchController = TextEditingController();

    final List<ExhibitionCardDTO> exhibitionList = [
      ExhibitionCardDTO(
        imageUrl: 'assets/Banner_Images/Banner_Image-1.jpeg',
        venue: 'Expo Center, Hall 5',
        startDate: 'December 15, 2024',
        endDate: 'December 16, 2024',
        eventStatus: 'Ongoing',
      ),
      ExhibitionCardDTO(
        imageUrl: 'assets/Banner_Images/Banner_Image-2.png',
        venue: 'Convention Center, Main Hall',
        startDate: 'January 10, 2025',
        endDate: 'January 10, 2025',
        eventStatus: 'Expired',
      ),
      ExhibitionCardDTO(
        imageUrl: 'assets/Banner_Images/Banner_Image-1.jpeg',
        venue: 'Grand Arena, East Wing',
        startDate: 'February 20, 2025',
        endDate: 'February 20, 2025',
        eventStatus: 'Expired',
      ),
    ];

    return Scaffold(
      body: Container(
        color: ColorPalette.lightBackgroundColor, // Set background color
        child: Column(
          children: [
            Header(
                searchController: searchController), // Removed extra Container
            Expanded(
              child: Stack(
                children: [
                  ListView.builder(
                    padding: EdgeInsets.zero, // Removed extra padding
                    itemCount: exhibitionList.length,
                    itemBuilder: (context, index) {
                      return Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: GestureDetector(
                          onTap: () {
                            // Navigate to the details page and pass the model
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (context) => ExhibitionDetailsPage(
                                  bannerImageUrl: exhibitionList[index]
                                      .imageUrl, // Use model data
                                  venuePhotos: [
                                    'assets/Banner_Images/Banner_Image-2.png',
                                    'assets/Banner_Images/Banner_Image-2.png',
                                    'assets/Banner_Images/Banner_Image-2.png',
                                    'assets/Banner_Images/Banner_Image-2.png'
                                  ],
                                  venueName: exhibitionList[index].venue,
                                  address: "A-1003,Statyajeet Sopan,Rajkot",
                                  timeDuration:
                                      "${exhibitionList[index].startDate} - ${exhibitionList[index].endDate}",
                                  numberOfTables: 50,
                                  description: "Good Exhibition",
                                ),
                              ),
                            );
                          },
                          child: ExhibitionDisplayCard(
                            model: exhibitionList[index],
                          ),
                        ),
                      );
                    },
                  ),
                  Positioned(
                    bottom: 16,
                    right: 16,
                    child: FloatingActionButton(
                      onPressed: () => FilterBtn.showFilterDialog(context),
                      backgroundColor: Colors.grey,
                      child: const Icon(Icons.filter_list),
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
